using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Azure.Cosmos;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;

namespace LearningProject1.Repository.Implementation
{
    public class BlobRepository : IBlobRepository
    {
        readonly BlobServiceClient _blobClient;
        private readonly BlobContainerClient _containerClient;

        IConfiguration _configuration;
        public BlobRepository(BlobServiceClient blobClient, IConfiguration configuration)
        {
            _blobClient = blobClient;
            _configuration = configuration;
            _containerClient = _blobClient.GetBlobContainerClient(_configuration["BlobStorage:Container"]);
            _containerClient.CreateIfNotExists();
        }

        public async Task<bool> Upload(IFormFile file)
        {
            string x = file.Name;
            BlobClient blobClient = _containerClient.GetBlobClient(file.FileName);

           // using (FileStream fileStream = (FileStream)file.OpenReadStream())
            {
                await blobClient.UploadAsync(file.OpenReadStream(), true);
            }
            return true;
        }

        public async Task<long> download(string filename)
        {
            BlobClient blobClient = _containerClient.GetBlobClient(filename);
            BlobDownloadInfo download = await blobClient.DownloadAsync();
            var x =download.ContentLength;  
            download.Dispose();
            return x;
        }
    }
}

