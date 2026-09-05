using System.Net;
using System.Text.Json;
using LearnFlowERP.Application.Common.Exceptions;
using LearnFlowERP.Application.Common.Models;

namespace LearnFlowERP.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled Exception");

                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            int statusCode;
            string message;
            string errorCode;

            switch (ex)
            {
                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = "Unauthorized access";
                    errorCode = "AUTH_401";
                    break;

                case NotFoundException:
                    statusCode = 404;
                    message = ex.Message;
                    errorCode = "GEN_404";
                    break;

                case ArgumentException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = ex.Message;
                    errorCode = "GEN_400";
                    break;

                case DataAlreadyExistsException:
                    statusCode = (int)HttpStatusCode.Conflict;
                    message = ex.Message;
                    errorCode = "GEN_409";
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "Something went wrong";
                    errorCode = "GEN_500";
                    break;
            }

            var correlationId = context.Items["CorrelationId"]?.ToString();

            var response = ApiResponse<string>.Fail(message, errorCode);
            response.CorrelationId = correlationId;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(response, JsonOptions));
        }
    }
}
