using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Workspace.Application.Common.Interfaces;

namespace Workspace.Infrastructure.Storage;

public class LocalFileStorageService : IFileStorageService
{
    private readonly string _uploadDirectory;

    public LocalFileStorageService(IConfiguration configuration)
    {
        var configuredPath = configuration["StorageSettings:Local:UploadPath"] ?? "wwwroot/uploads/workspace";
        
        // Si el path no es absoluto, lo combinamos con el directorio de ejecución actual
        _uploadDirectory = Path.IsPathRooted(configuredPath) 
            ? configuredPath 
            : Path.Combine(Directory.GetCurrentDirectory(), configuredPath);

        if (!Directory.Exists(_uploadDirectory))
        {
            Directory.CreateDirectory(_uploadDirectory);
        }
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken)
    {
        // Generar un nombre único para evitar colisiones
        var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(fileName)}";
        var fullPath = Path.Combine(_uploadDirectory, uniqueFileName);

        using (var destinationStream = new FileStream(
            fullPath, 
            FileMode.Create, 
            FileAccess.Write, 
            FileShare.None, 
            4096, 
            useAsync: true))
        {
            await fileStream.CopyToAsync(destinationStream, cancellationToken);
        }

        // Retornar ruta web relativa para el acceso al recurso
        return $"/uploads/workspace/{uniqueFileName}";
    }
}
