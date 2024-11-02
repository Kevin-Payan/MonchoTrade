namespace monchotradebackend.service;
using monchotradebackend.Interface;

public enum UploadType
{
    Profile,
    Product
}
public class FileService : IFileService
{
    private readonly IWebHostEnvironment environment;

    public FileService(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }

    private string GetUploadPath(UploadType uploadType)
    {
        var contentPath = environment.ContentRootPath;
        var baseUploadPath = Path.Combine(contentPath, "uploads");
        
        return uploadType switch
        {
            UploadType.Profile => Path.Combine(baseUploadPath, "profiles"),
            UploadType.Product => Path.Combine(baseUploadPath, "products"),
            _ => throw new ArgumentException("Invalid upload type")
        };
    }

    public async Task<string> SaveFileAsync(IFormFile File, string[] allowedFileExtensions, UploadType uploadType)
    {
        if (File == null)
        {
            throw new ArgumentNullException(nameof(File));
        }

        var path = GetUploadPath(uploadType);
        
        // Create directory if it doesn't exist
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        var ext = Path.GetExtension(File.FileName);
        if (!allowedFileExtensions.Contains(ext))
        {
            throw new ArgumentException($"Only {string.Join(",", allowedFileExtensions)} are allowed");
        }

        var fileName = $"{Guid.NewGuid()}{ext}";
        var fileNameWithPath = Path.Combine(path, fileName);

        using var stream = new FileStream(fileNameWithPath, FileMode.Create);
        await File.CopyToAsync(stream);

        return fileName;
    }

    public void DeleteFile(string fileNameWithExtension, UploadType uploadType)
    {
        if (string.IsNullOrEmpty(fileNameWithExtension))
        {
            throw new ArgumentNullException(nameof(fileNameWithExtension));
        }

        var path = GetUploadPath(uploadType);
        var fullPath = Path.Combine(path, fileNameWithExtension);

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException($"Invalid File path");
        }

        File.Delete(fullPath);
    }
}