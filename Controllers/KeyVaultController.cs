using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace LearningProject1.Controllers
{

    [Route("api/")]
    [ApiController]
    public class KeyVaultController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public KeyVaultController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("secrets/{secretName}")]
        public string Get(string secretName)
        {
            var secretValue = _configuration[secretName];
            return secretValue;
        }
    }
}
