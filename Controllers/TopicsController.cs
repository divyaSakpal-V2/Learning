using LearningProject1.ServiceLayer;
using LearningProject1.ServiceLayer.DTOs;
using LearningProject1.ServiceLayer.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningProject1.Controllers
{

    [Route("api/")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        readonly ITopicService _topicService;
        readonly ILogger<TopicsController> _logger;


        public TopicsController(ITopicService topicService, ILogger<TopicsController> logger)
        {
            _topicService = topicService;
            _logger = logger;

        }

        [HttpGet("/topics")]
        public async Task<IActionResult> GetAllTopics()
        {
            try
            {
                _logger.LogInformation("get all topics ");
                var  result = await _topicService.GetAllTopics();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Topics/search/{name}")]
        public async Task<IActionResult> searchTopics(string name)
        {
            try
            {
                _logger.LogInformation("search for topic " + name);
                var result = await _topicService.searchTopics(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLinks()
        {
            try
            {
                _logger.LogInformation("All links request");
                var result = await _topicService.GetAllLinks();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Links/byTopic/{name}")]
        public async Task<IActionResult> searchLinks(string name)
        {
            try
            {
                _logger.LogInformation("search links request" + name);
                var  result = await _topicService.searchLinks(name);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveLink(LinkDto linkDto)
        {
            try
            {
                _logger.LogInformation("save link request for topic "+ linkDto.Topic );
                var result = await _topicService.SaveLink(linkDto);
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
