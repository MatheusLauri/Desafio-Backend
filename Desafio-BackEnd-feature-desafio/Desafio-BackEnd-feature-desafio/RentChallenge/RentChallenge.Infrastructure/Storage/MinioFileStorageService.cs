using FluentValidation;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using RentChallenge.Domain.Interfaces.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentChallenge.Infrastructure.Storage
{
    public class MinioFileStorageService : IFileStorageService
    {
        private readonly IMinioClient _minioClient;
        private readonly string _bucketName = "cnh-files";

        public MinioFileStorageService(string endpoint, string accessKey, string secretKey)
        {
            _minioClient = new MinioClient()
                .WithEndpoint("minio", 9000)
                .WithCredentials(accessKey, secretKey)
            .Build();

            _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
        }


        public async Task<string> UploadAsync(Stream fileStream, string fileName, string contentType)
        {
            var allowedTypes = new[] { "image/png", "image/bmp" };
            var allowedExtensions = new[] { ".png", ".bmp" };

            if (!allowedTypes.Contains(contentType.ToLower()) &&
                !allowedExtensions.Contains(Path.GetExtension(fileName).ToLower()))
            {
                throw new ValidationException("Dados inválidos ");
            }

            var args = new PutObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType);

            await _minioClient.PutObjectAsync(args);

            return $"{_bucketName}/{fileName}";
        }

        public async Task<Stream?> GetAsync(string fileName)
        {
            var ms = new MemoryStream();
            await _minioClient.GetObjectAsync(new GetObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName)
                .WithCallbackStream(stream => stream.CopyTo(ms)));
            ms.Position = 0;
            return ms;
        }

        public async Task DeleteAsync(string fileName)
        {
            await _minioClient.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(_bucketName)
                .WithObject(fileName));
        }
    }
}
