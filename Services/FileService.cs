using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;

namespace api.Services
{
  public class FileService : IFileService
  {
    private readonly IWebHostEnvironment _webHostEnvironment;
    public FileService(IWebHostEnvironment webHostEnvironment)
    {
      _webHostEnvironment = webHostEnvironment;
    }
    public async Task<string> SaveFileAsync(IFormFile formFile)
    {
      if (formFile == null)
      {
        throw new ArgumentNullException(nameof(formFile));
      }

      var contentPath = _webHostEnvironment.ContentRootPath;
      var path = Path.Combine(contentPath, "images");

      string[] allowedFileExtensions = [".jpg", ".png", ".jpeg"];

      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }

      var extension = Path.GetExtension(formFile.FileName);
      if (!allowedFileExtensions.Contains(extension))
      {
        throw new ArgumentException($"Only {string.Join(',', allowedFileExtensions)} are allowed");
      }

      var filename = $"{Guid.NewGuid().ToString()}{extension}";
      var filenameWithPath = Path.Combine(path, filename);
      using var stream = new FileStream(filenameWithPath, FileMode.Create);
      await formFile.CopyToAsync(stream);

      return filename;
    }
  }
}