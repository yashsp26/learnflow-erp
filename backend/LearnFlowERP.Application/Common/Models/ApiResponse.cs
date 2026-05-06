
namespace LearnFlowERP.Application.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }   // ✅ nullable
        public string Message { get; set; }
        public string? ErrorCode { get; set; }  // 🔥 optional but powerful
        public string? CorrelationId { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string message = "")
            => new() { Success = true, Data = data, Message = message };

        public static ApiResponse<T> Fail(string message, string? errorCode = null)
            => new() { Success = false, Message = message, ErrorCode = errorCode };
    }
}