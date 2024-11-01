namespace monchotradebackend.Interface; 
public interface IFileService{
    Task<string> SaveFileAsync(IFormFile File, string[] allowedFileExtensions);
    void DeleteFile(string fileNameWithExtension); 
}