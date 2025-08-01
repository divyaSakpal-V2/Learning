namespace LearningProject1.Repository
{
    public interface IBlobRepository
    {
          Task<bool> Upload(IFormFile file);
        Task<long> download(string filename);
    }
}
