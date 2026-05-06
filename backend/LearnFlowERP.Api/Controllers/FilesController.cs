using LearnFlowERP.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LearnFlowERP.Api.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController : ControllerBase
    {
        private readonly IFileStorageService _fileService;

        public FilesController(IFileStorageService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            // 🔐 Validation
            if (file.Length > 5 * 1024 * 1024)
                return BadRequest("File too large");

            var fileName = $"{Guid.NewGuid()}_{file.FileName}";

            using var stream = file.OpenReadStream();

            var url = await _fileService.UploadAsync(
                stream,
                fileName,
                file.ContentType);

            return Ok(url);
        }
    }
}
