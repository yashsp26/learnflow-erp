using LearnFlowERP.Api;
using LearnFlowERP.Api.Middleware;
using LearnFlowERP.Application.Common;
using LearnFlowERP.Application.Common.Behaviors;
using LearnFlowERP.Application.Common.Interfaces;
using LearnFlowERP.Infrastructure.Persistence.AppDbContext;
using LearnFlowERP.Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LearnFlowERP.Infrastructure.Services;
using LearnFlowERP.Application.Features.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;
using FluentValidation;
using LearnFlowERP.Application.Common.Models;
using LearnFlowERP.Infrastructure.Notifications;
using Microsoft.AspNetCore.Authorization;
using LearnFlowERP.Api.Authorization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using LearnFlowERP.Api.Http;
using LearnFlowERP.Infrastructure.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string"
        + "'DefaultConnection' not found.");

// Services

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions =>
        {
            sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        }));

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<AppDbContext>());

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "LearnFlowERP API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddHttpContextAccessor();
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddScoped<ICurrentUserService, FakeCurrentUserService>();
}
else
{
    builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
}




// JWT Authentication
builder.Services.AddScoped<ITokenService, JwtTokenService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddAuthentication("Bearer")
        .AddScheme<AuthenticationSchemeOptions, FakeAuthHandler>("Bearer", null);
}
else
{
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            options.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    context.HandleResponse();

                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(
                        ApiResponse<string>.Fail("Unauthorized access")
                    );
                },

                OnForbidden = async context =>
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsJsonAsync(
                        ApiResponse<string>.Fail("Forbidden: Access denied")
                    );
                }
            };
        });
}


// Email
builder.Services.AddHttpClient<IEmailService, BrevoEmailService>();

// Permissions

builder.Services.AddAuthorization();

builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

// Validators

builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// JSON Serialization
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});


// Pipeline Behaviors
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehavior<,>));

builder.Services.AddAppDI();
builder.Services.AddMediatR(typeof(ApplicationAssemblyMarker).Assembly);

builder.Services.AddScoped<PasswordHasher>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IPermissionCacheService, PermissionCacheService>();


builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("LoginPolicy", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Request.Headers["X-Forwarded-For"].ToString() ?? "global",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,              // max 5 requests
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));

    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

// Supabase
builder.Services.AddSingleton<IFileStorageService, SupabaseFileStorageService>();

// Serilog

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .Enrich.WithMachineName()
    .WriteTo.Console(outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level:u3}] [CorrId: {CorrelationId}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient<CorrelationIdHandler>();

builder.Services.AddHttpClient("MyClient").AddHttpMessageHandler<CorrelationIdHandler>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "LearnFlowERP API v1");
        options.RoutePrefix = "swagger"; // optional but clean
    });
//}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

// Middleware
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<CorrelationIdMiddleware>();
app.UseMiddleware<ResponseWrapperMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<TenantMiddleware>();
app.UseRateLimiter();


app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
