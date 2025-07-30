using LearningProject1.ServiceLayer.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LearningProject1.ServiceLayer
{
    public interface ITopicService
    {
        public Task<List<string>> GetAllTopics();


        public Task<List<string>> searchTopics(string name);


        public Task<List<LinkDto>> GetAllLinks();


        public Task<List<LinkDto>> searchLinks(string name);


        public Task<bool> SaveLink(LinkDto linkDto);

    }
}
