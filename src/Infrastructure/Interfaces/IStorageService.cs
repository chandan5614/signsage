using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IStorageService
    {
        Task<string> UploadFileAsync(string containerName, Stream fileStream, string fileName);
        Task<Stream> DownloadFileAsync(string containerName, string fileName);
        Task<bool> DeleteFileAsync(string containerName, string fileName);
        Task<bool> FileExistsAsync(string containerName, string fileName);
    }
}
