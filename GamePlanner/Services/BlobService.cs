using Azure.Identity;
using Azure.Storage.Blobs;
using GamePlanner.DTO.ConfigurationDTO;
using GamePlanner.Helpers;
using GamePlanner.Services.IServices;
using Microsoft.Identity.Client;

namespace GamePlanner.Services
{
    public class BlobService : IBlobService
    {
        private readonly string _defaultConnectionString = KeyVaultHelper.GetSecrectConnectionString("StorageAccountConnection");

        public BlobContainerClient GetBlobContainerClient(string containerName)
        {
            ArgumentException.ThrowIfNullOrEmpty(_defaultConnectionString);

            return new BlobContainerClient(_defaultConnectionString, containerName);
        }
        public BlobContainerClient GetBlobContainerClient(string connectionString, string containerName)
        {
            return new BlobContainerClient(connectionString, containerName);
        }

        public async Task<string> UploadFileAsync(BlobContainerClient containerClient, IFormFile file)
        {
            await containerClient.CreateIfNotExistsAsync();

            var blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            await using var stream = file.OpenReadStream();
            await blobClient.UploadAsync(stream);

            return blobName;
        }

        public async Task<Stream> DownloadBlobToStreamAsync(BlobContainerClient containerClient, string fileName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                using var memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream);
                memoryStream.Position = 0;

                return memoryStream;
            }

            throw new FileNotFoundException("File not found in blob storage.");
        }
    }
}
