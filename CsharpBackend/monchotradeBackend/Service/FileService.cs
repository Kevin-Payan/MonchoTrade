namespace monchotradebackend.service;
using monchotradebackend.Interface;

public class FileService(IWebHostEnvironment environment) : IFileService{
    public async Task<string> SaveFileAsync(IFormFile File, string[] allowedFileExtensions){
        if(File == null){
            throw new ArgumentNullException(nameof(File));
        }

        var contentPath = environment.ContentRootPath; 
        var path = Path.Combine(contentPath, "uploads"); 

        if(!Directory.Exists(path)){
            Directory.CreateDirectory(path); 
        }

        var ext = Path.GetExtension(File.FileName); 

        if(!allowedFileExtensions.Contains(ext)){
            throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed");
        }

        var fileName = $"{Guid.NewGuid()}{ext}";
        var fileNameWithPath = Path.Combine(path,fileName); 
        using var stream = new FileStream(fileNameWithPath, FileMode.Create); 
        await File.CopyToAsync(stream); 
        return fileName; 
    }


    public void DeleteFile(string fileNameWithExtension){
        if(string.IsNullOrEmpty(fileNameWithExtension)){
            throw new ArgumentNullException(nameof(fileNameWithExtension));
        }
        
        var contentPath = environment.ContentRootPath;
        var path = Path.Combine(contentPath, $"uploads", fileNameWithExtension);

        if(!File.Exists(path)){
            throw new FileNotFoundException($"Invalid File path");
        }

        File.Delete(path);
    }
}