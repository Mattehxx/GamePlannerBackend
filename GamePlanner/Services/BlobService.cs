﻿using Azure.Identity;
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

        public string UploadFile(BlobContainerClient containerClient, IFormFile file)
        {
            containerClient.CreateIfNotExists(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            var blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using var stream = file.OpenReadStream();
            blobClient.Upload(stream);

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task<string> UploadFileAsync(BlobContainerClient containerClient, IFormFile file)
        {
            await containerClient.CreateIfNotExistsAsync(Azure.Storage.Blobs.Models.PublicAccessType.BlobContainer);

            var blobName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);

            using var stream = file.OpenReadStream();
            var response = await blobClient.UploadAsync(stream);

            return blobClient.Uri.AbsoluteUri;
        }

        public byte[] DownloadBlobToStream(BlobContainerClient containerClient, string fileName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            if (blobClient.Exists())
            {
                using var memoryStream = new MemoryStream();
                blobClient.DownloadTo(memoryStream);
                return memoryStream.ToArray();
            }

            throw new FileNotFoundException("File not found in blob storage.");
        }

        public async Task<byte[]> DownloadBlobToStreamAsync(BlobContainerClient containerClient, string fileName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            if (await blobClient.ExistsAsync())
            {
                using var memoryStream = new MemoryStream();
                await blobClient.DownloadToAsync(memoryStream);
                return memoryStream.ToArray();
            }

            throw new FileNotFoundException("File not found in blob storage.");
        }
    }
}
