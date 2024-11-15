using Azure.Storage.Blobs;

namespace GamePlanner.Services.IServices
{
    public interface IBlobService
    {
        public BlobContainerClient GetBlobContainerClient(string containerName);
        public BlobContainerClient GetBlobContainerClient(string connectionString, string containerName);
        public string UploadFile(BlobContainerClient containerClient, IFormFile file);
        public Task<string> UploadFileAsync(BlobContainerClient containerClient, IFormFile file);
        public byte[] DownloadBlobToStream(BlobContainerClient containerClient, string fileName);
        public Task<byte[]> DownloadBlobToStreamAsync(BlobContainerClient containerClient, string fileName);
    }
}
