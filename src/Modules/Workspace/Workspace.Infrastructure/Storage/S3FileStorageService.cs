using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Workspace.Application.Common.Interfaces;

namespace Workspace.Infrastructure.Storage;

public class S3FileStorageService : IFileStorageService
{
    private readonly string _bucketName;
    private readonly string _region;
    private readonly string? _serviceUrl;
    private readonly IAmazonS3 _s3Client;

    public S3FileStorageService(IConfiguration configuration)
    {
        _bucketName = configuration["StorageSettings:S3:BucketName"] 
            ?? throw new ArgumentNullException(nameof(configuration), "El BucketName de S3 no está configurado.");
        _region = configuration["StorageSettings:S3:Region"] ?? "us-east-1";
        _serviceUrl = configuration["StorageSettings:S3:ServiceUrl"];

        var accessKey = configuration["StorageSettings:S3:AccessKey"];
        var secretKey = configuration["StorageSettings:S3:SecretKey"];
        var regionEndpoint = RegionEndpoint.GetBySystemName(_region);

        if (!string.IsNullOrEmpty(_serviceUrl))
        {
            var config = new AmazonS3Config
            {
                ServiceURL = _serviceUrl,
                ForcePathStyle = true // Requerido para MinIO y emuladores locales
            };

            if (!string.IsNullOrEmpty(accessKey) && !string.IsNullOrEmpty(secretKey))
            {
                _s3Client = new AmazonS3Client(accessKey, secretKey, config);
            }
            else
            {
                _s3Client = new AmazonS3Client(config);
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(accessKey) && !string.IsNullOrEmpty(secretKey))
            {
                _s3Client = new AmazonS3Client(accessKey, secretKey, regionEndpoint);
            }
            else
            {
                _s3Client = new AmazonS3Client(regionEndpoint);
            }
        }
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)
    {
        // Auto-crear el bucket si no existe (ideal para entornos de desarrollo como MinIO)
        if (!await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _bucketName))
        {
            await _s3Client.PutBucketAsync(new PutBucketRequest { BucketName = _bucketName }, cancellationToken);
        }

        var uniqueFileName = $"workspace/{Guid.NewGuid()}_{Path.GetFileName(fileName)}";

        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = uniqueFileName,
            InputStream = fileStream,
            ContentType = "application/pdf"
        };

        var response = await _s3Client.PutObjectAsync(request, cancellationToken);

        if (response.HttpStatusCode != HttpStatusCode.OK)
        {
            throw new WebException($"Fallo la subida a S3 con código de estado HTTP: {response.HttpStatusCode}");
        }

        // Retornar la URL adecuada (URL local de MinIO o la oficial de AWS S3)
        if (!string.IsNullOrEmpty(_serviceUrl))
        {
            return $"{_serviceUrl.TrimEnd('/')}/{_bucketName}/{uniqueFileName}";
        }

        return $"https://{_bucketName}.s3.{_region}.amazonaws.com/{uniqueFileName}";
    }
}
