using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace LearningProject1.Repository.Models
{
    public class Link
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        public required string Topic { get; set; }
        public required string URL { get; set; }
        public string? Descriptions { get; set; }
    }
}
