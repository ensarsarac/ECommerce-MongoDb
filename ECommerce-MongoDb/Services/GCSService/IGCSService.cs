namespace ECommerce_MongoDb.Services.GCSService
{
    public interface IGCSService
    {
        Task<string> UploadFileAsync(IFormFile fileToUpload, string fileNameToSave);
        Task DeleteFileAsync(string fileNameToDelete);
    }
}
