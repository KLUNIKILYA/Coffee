using Microsoft.AspNetCore.Http;

namespace Coffee.Core.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);

        void DeleteFile(string filePath);
    }
}