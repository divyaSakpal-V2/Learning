namespace LearningProject1.ServiceLayer.DTOs
{
    public class LinkDto
    {
        public Guid Id { get; set; }
        public required string Topic { get; set; }
        public required string URL { get; set; }
        public string? Descriptions  { get; set; }
    }
}
