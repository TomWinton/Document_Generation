using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using System;

namespace DocumentGeneration.Blob
    {
    public partial class Blob
        {
        private ILogger _log;
        private BlobServiceClient blobService;
        private BlobContainerClient blobContainerClient;
        public Blob(ILogger log)
            {
                _log = log;
                blobService = new BlobServiceClient(Environment.GetEnvironmentVariable("BlobConnectionString"));
                blobContainerClient = blobService.GetBlobContainerClient(Environment.GetEnvironmentVariable("ContainerName"));
            }
        }
    }