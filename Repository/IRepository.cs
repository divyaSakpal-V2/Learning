using LearningProject1.Repository.Models;

namespace LearningProject1.Repository
{
    public interface IRepository
    {
        Task<List<Link>> Getlinks();
        Task<List<string>> GetTopics();
        Task<List<Link>> Searchlinks(string name);
        Task<List<string>> SearchTopics(string name);
        Task<bool> Save(Link link);
    }
}
