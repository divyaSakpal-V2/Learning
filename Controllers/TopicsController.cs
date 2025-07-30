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
        public readonly ITopicService _topicService;
       
        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet("/topics")]
        public async Task<List<string>> GetAllTopics()
        {
            return await _topicService.GetAllTopics();
        }
        [HttpGet("/Topics/search/{name}")]
        public async Task<List<string>> searchTopics(string name)
        {
            return await _topicService.searchTopics(name);
        }

        [HttpGet]
        public async Task<List<LinkDto>> GetAllLinks()
        {
            return await _topicService.GetAllLinks();
        }

        [HttpGet("/Links/byTopic/{name}")]
        public async Task<List<LinkDto>> searchLinks(string name)
        {
            return await _topicService.searchLinks(name);
        }

        [HttpPost]
        public async Task<bool> SaveLink(LinkDto linkDto)
        {
            return await _topicService.SaveLink(linkDto);
        }

    }
}
