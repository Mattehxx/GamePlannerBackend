using Azure.Storage.Blobs;

namespace GamePlanner.Services.IServices
{
    public interface IBlobService
    {
        public BlobContainerClient GetBlobContainerClient(string containerName);
        public BlobContainerClient GetBlobContainerClient(string connectionString, string containerName);
        public Task<string> UploadFileAsync(BlobContainerClient containerClient, IFormFile file);
        public Task<byte[]> DownloadBlobToStreamAsync(BlobContainerClient containerClient, string fileName);
    }
}
