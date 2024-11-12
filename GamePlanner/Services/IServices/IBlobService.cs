using Azure.Storage.Blobs;

namespace GamePlanner.Services.IServices
{
    public interface IBlobService
    {
        public BlobContainerClient GetBlobContainerClient(string connectionString, string containerName);
        public Task<string> UploadFileAsync(BlobContainerClient containerClient, IFormFile file);
        public Task<Stream> DownloadBlobToStreamAsync(BlobContainerClient containerClient, string fileName);
    }
}
