using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearningProject1.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        List<string> Topics;
        public TopicsController()
        {
            Topics = new List<string>() { "Azure", "Dot net" };
        }

        // GET: api/<TopicsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Topics;
        }

        // GET api/<TopicsController>/5
        [HttpGet("{name}")]
        public IEnumerable<string> Get(string name)
        {
            return Topics.FindAll(x => x.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
