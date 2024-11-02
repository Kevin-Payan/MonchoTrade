namespace monchotradebackend.Interface;
using monchotradebackend.service;
public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile File, string[] allowedFileExtensions, UploadType uploadType);
    void DeleteFile(string fileNameWithExtension, UploadType uploadType);
}