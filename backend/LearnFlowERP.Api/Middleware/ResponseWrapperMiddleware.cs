using System.Text.Json;
using LearnFlowERP.Application.Common.Models;

namespace LearnFlowERP.Api.Middleware
{
public class ResponseWrapperMiddleware
{
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly RequestDelegate _next;

        public ResponseWrapperMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            try
            {
                await _next(context);

                var correlationId = context.Items["CorrelationId"]?.ToString();
                var contentType = context.Response.ContentType;

                // if response already started, DO NOT TOUCH
                if (context.Response.HasStarted)
                {
                    return;
                }

                // Skip NON-API routes
                if (!context.Request.Path.StartsWithSegments("/api"))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(originalBodyStream);
                    return;
                }

                // Skip non-JSON (files, streams)
                if (!string.IsNullOrEmpty(contentType) &&
                    (contentType.StartsWith("application/octet-stream") ||
                     contentType.StartsWith("image/") ||
                     contentType.StartsWith("video/") ||
                     contentType.StartsWith("application/pdf")))
                {
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    await memoryStream.CopyToAsync(originalBodyStream);
                    return;
                }

                // Skip 204
                if (context.Response.StatusCode == StatusCodes.Status204NoContent)
                {
                    context.Response.Body = originalBodyStream;
                    return;
                }

                memoryStream.Seek(0, SeekOrigin.Begin);
                var bodyText = await new StreamReader(memoryStream).ReadToEndAsync();

                // EMPTY RESPONSE FIX
                if (string.IsNullOrWhiteSpace(bodyText))
                {
                    var emptyResponse = new ApiResponse<object>
                    {
                        Success = context.Response.StatusCode < 400,
                        Data = null,
                        Message = "",
                        CorrelationId = correlationId
                    };

                    context.Response.Body = originalBodyStream;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(
                        JsonSerializer.Serialize(emptyResponse, JsonOptions));
                    return;
                }

                object finalResponse;
                JsonElement? parsedJson = null;

                // SAFE JSON PARSE
                try
                {
                    parsedJson = JsonSerializer.Deserialize<JsonElement>(bodyText);
                }
                catch
                {
                    // not JSON → fallback
                }

                // Already wrapped
                if (parsedJson.HasValue &&
                    parsedJson.Value.ValueKind == JsonValueKind.Object &&
                    parsedJson.Value.TryGetProperty("success", out _))
                {
                    var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(
                        bodyText,
                        JsonOptions)!;
                    dict["correlationId"] = correlationId;
                    finalResponse = dict;
                }
                else
                {
                    object data;

                    if (parsedJson.HasValue)
                    {
                        var kind = parsedJson.Value.ValueKind;

                        if (kind == JsonValueKind.Object ||
                            kind == JsonValueKind.Array ||
                            kind == JsonValueKind.String ||
                            kind == JsonValueKind.Number ||
                            kind == JsonValueKind.True ||
                            kind == JsonValueKind.False)
                        {
                            data = parsedJson.Value;
                        }
                        else
                        {
                            data = bodyText;
                        }
                    }
                    else
                    {
                        data = bodyText;
                    }

                    finalResponse = new ApiResponse<object>
                    {
                        Success = context.Response.StatusCode < 400,
                        Data = data,
                        Message = "",
                        CorrelationId = correlationId
                    };
                }

                context.Response.Body = originalBodyStream;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(
                    JsonSerializer.Serialize(finalResponse, JsonOptions));
            }
            finally
            {
                context.Response.Body = originalBodyStream;
            }
        }
    }
}
