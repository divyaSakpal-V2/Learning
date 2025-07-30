using LearningProject1.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Configuration;

namespace LearningProject1.Repository.Implementation
{
    public class Repository : IRepository
    {
        readonly CosmosClient _cosmosClient;
        Database database;
        Container container;
        IConfiguration _configuration;
        public Repository(CosmosClient cosmosClient, IConfiguration configuration) 
        {
            _cosmosClient = cosmosClient;
            _configuration = configuration;
            database =  cosmosClient.CreateDatabaseIfNotExistsAsync(_configuration["CosmosDb:Database"]).Result;
            container =  database.CreateContainerIfNotExistsAsync(_configuration["CosmosDb:Container"], "/Topic").Result;
        }
        public async Task<List<Link>> Getlinks()
        {
            List <Link> links = new List<Link>() ;
            // Query multiple items from container
            using FeedIterator<Link> feed = container.GetItemQueryIterator<Link>(
                queryText: "SELECT * FROM C");

            // Iterate query result pages
            while (feed.HasMoreResults)
            {
                FeedResponse<Link> response =  feed.ReadNextAsync().Result;
                links.AddRange(response);
            }
            return links;
        }

        public async Task<List<string>> GetTopics()
        { 
            List<string> result = new List<string>();
            // Query multiple items from container
            using FeedIterator<string> feed = container.GetItemQueryIterator<string>(
                queryText: "SELECT Distinct Value(C.Topic ) FROM C");

            // Iterate query result pages
            while (feed.HasMoreResults)
            {
                var response =  feed.ReadNextAsync().Result;
                result.AddRange(response);
            }
            return result;
        }

        public async Task<bool> Save(Link link)
        {
            if (link.Id == Guid.Empty)
                link.Id = Guid.NewGuid();

            Link upsertedItem = await container.UpsertItemAsync<Link>(
                item: link,
                partitionKey: new PartitionKey(link.Topic)
            );
            return true;
        }

        public async Task<List<Link>> Searchlinks(string name)
        {
            List<Link> links = new List<Link>();
            // Define a parameterized query
            string queryText = "SELECT Distinct * FROM C WHERE Lower(C.Topic) like @Topic ";
            QueryDefinition queryDefinition = new QueryDefinition(queryText)
                .WithParameter("@Topic", "%" + name.ToLower() + "%");

            // Query multiple items from container
            using FeedIterator<Link> feed = container.GetItemQueryIterator<Link>(queryDefinition);

            // Iterate query result pages
            while (feed.HasMoreResults)
            {
                FeedResponse<Link> response = feed.ReadNextAsync().Result;
                links.AddRange(response);
            }
            return links;
        }

        public async Task<List<string>> SearchTopics(string name)
        {
            List<string> result = new List<string>();
            // Define a parameterized query
            string queryText = "SELECT Distinct Value(C.Topic ) FROM C WHERE Lower(C.Topic) like @Topic ";
            QueryDefinition queryDefinition = new QueryDefinition(queryText)
                .WithParameter("@Topic", "%"+name.ToLower()+"%");

            // Query multiple items from container
            using FeedIterator<string> feed = container.GetItemQueryIterator<string>(queryDefinition);

            // Iterate query result pages
            while (feed.HasMoreResults)
            {

                var response = feed.ReadNextAsync().Result;
                result.AddRange(response);
            }
            return result;
        }
        public async Task<List<T>> ConvertFeedResponseToListAsync<T>(FeedIterator<T> feedIterator)
        {
            var results = new List<T>();

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<T> response = await feedIterator.ReadNextAsync();
                results.AddRange(response); // Adds all items from the current page
            }

            return results;
        }
    }
}
