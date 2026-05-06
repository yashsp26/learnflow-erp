//using LearnFlowERP.Application.Common.Models;
//using Microsoft.AspNetCore.Mvc;

//namespace LearnFlowERP.Api.Controllers
//{
//    public abstract class BaseController : ControllerBase
//    {
//        protected IActionResult Success<T>(T data, string message = "")
//        {
//            var response = ApiResponse<T>.SuccessResponse(data, message);

//            response.CorrelationId = HttpContext.Items["CorrelationId"]?.ToString();

//            return Ok(response);
//        }

//        protected IActionResult Failure(string message)
//        {
//            var response = ApiResponse<string>.Fail(message);

//            response.CorrelationId = HttpContext.Items["CorrelationId"]?.ToString();

//            return BadRequest(response);
//        }
//    }
//}
