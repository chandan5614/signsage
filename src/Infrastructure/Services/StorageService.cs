using Infrastructure.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class StorageService : IStorageService
    {
        private readonly string _storageDirectory;

        public StorageService(string storageDirectory)
        {
            _storageDirectory = storageDirectory;
            
            // Ensure the directory exists
            if (!Directory.Exists(_storageDirectory))
            {
                Directory.CreateDirectory(_storageDirectory);
            }
        }

        public async Task<string> UploadFileAsync(byte[] fileData, string fileName)
        {
            var filePath = Path.Combine(_storageDirectory, fileName);

            // Write the file to the storage directory
            await File.WriteAllBytesAsync(filePath, fileData);

            return filePath;
        }

        public async Task<byte[]> DownloadFileAsync(string fileName)
        {
            var filePath = Path.Combine(_storageDirectory, fileName);

            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File '{fileName}' not found.");
            }

            // Read the file data
            var fileData = await File.ReadAllBytesAsync(filePath);
            return fileData;
        }

        public bool DeleteFile(string fileName)
        {
            var filePath = Path.Combine(_storageDirectory, fileName);

            // Check if the file exists and delete it
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }
    }
}
