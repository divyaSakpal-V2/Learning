using LearningProject1.Repository;
using LearningProject1.Repository.Models;
using LearningProject1.ServiceLayer.DTOs;

namespace LearningProject1.ServiceLayer.Implementation
{
    public class TopicService : ITopicService
    {
        public readonly IRepository _repo;
        public TopicService(IRepository repo) { 
            _repo = repo;
        }
        public async Task<List<LinkDto>> GetAllLinks()
        {
            List <LinkDto>  linkDtos = new List<LinkDto>();
            List<Link> result  = await  _repo.Getlinks();
            foreach(var link in result)
            {
                LinkDto linkDto =new LinkDto() { Id= link.Id , Descriptions =link.Descriptions,
                URL= link.URL, Topic=link.Topic};

                linkDtos.Add(linkDto);
            }
            return linkDtos;
        }

        public async Task<List<string>> GetAllTopics()
        {
            return await _repo.GetTopics();  
        }

        public async Task<bool> SaveLink(LinkDto linkDto)
        {
            Link link = new Link() { Id= linkDto.Id, 
                Descriptions= linkDto.Descriptions,
                Topic= linkDto.Topic, URL=linkDto.URL };
            return await _repo.Save(link);
        }

        public async Task<List<LinkDto>> searchLinks(string name)
        {
            List<LinkDto> linkDtos = new List<LinkDto>();
            List<Link> result = await _repo.Searchlinks(name);
            foreach (var link in result)
            {
                LinkDto linkDto = new LinkDto()
                {
                    Id = link.Id,
                    Descriptions = link.Descriptions,
                    URL = link.URL,
                    Topic = link.Topic
                };

                linkDtos.Add(linkDto);
            }
            return linkDtos;
        }

        public async Task<List<string>> searchTopics(string name)
        {
            return await _repo.SearchTopics(name);
        }
    }
}
