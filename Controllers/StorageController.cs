using LearningProject1.Repository;
using LearningProject1.ServiceLayer;
using LearningProject1.ServiceLayer.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace LearningProject1.Controllers
{
    [Route("api/")]
    [ApiController]
    public class StorageController : Controller
    {
        readonly IBlobRepository _service;
        readonly ILogger<StorageController> _logger;


        public StorageController(IBlobRepository service, ILogger<StorageController> logger)
        {
            _service = service;
            _logger = logger;

        }
        [HttpGet("/CountLength")]
        public async Task<IActionResult> GetFile(string name)
        {
            try
            {
                _logger.LogInformation("File content counting request for "+ name);
                var result = await _service.download(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddFile( IFormFile file)
        {
            try
            {
                _logger.LogInformation("File upload request for " + file.FileName);
                var result = await _service.Upload(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
