
using ECommerce_MongoDb.Models;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;

namespace ECommerce_MongoDb.Services.GCSService
{
    public class GCSService : IGCSService
    {
        private readonly GoogleCredential googleCredential;
        private readonly StorageClient storageClient;
        private readonly string bucketName;

        public GCSService(IConfiguration configuration)
        {
            googleCredential = GoogleCredential.FromFile(configuration.GetValue<string>("GCPStorageAuthFile"));
            storageClient = StorageClient.Create(googleCredential);
            bucketName = configuration.GetValue<string>("GoogleCloudStorageBucketName");
        }
        public async Task DeleteFileAsync(string fileNameToDelete)
        {
            await storageClient.DeleteObjectAsync(bucketName, fileNameToDelete);
        }

        public async Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave)
        {
            using (var memoryStream = new MemoryStream())
            {
                await fileToUpload.CopyToAsync(memoryStream);
                var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameToSave, null, memoryStream);
                return dataObject.MediaLink;
            }
        }
    }
}
