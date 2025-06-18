using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Domain.Interfaces.Storage
{
    public interface IFileStorageService
    {
        Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);
        Task<Stream?> GetAsync(string fileName);
        Task DeleteAsync(string fileName);
    }
}
