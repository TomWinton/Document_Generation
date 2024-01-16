using Azure.Storage.Blobs;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace DocumentGeneration.Blob
    {
    public partial class Blob
        {

        public byte[] retrieveFileFromBlob(string blobPath)
            {
      
                var blobClient = blobContainerClient.GetBlobClient(blobPath);
                using (var ms = new MemoryStream())
                    {
                    blobClient.DownloadTo(ms);
                    return ms.ToArray();
                    }  
            }
        }
    }