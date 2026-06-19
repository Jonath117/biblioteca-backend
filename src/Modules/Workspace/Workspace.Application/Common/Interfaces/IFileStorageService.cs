using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Workspace.Application.Common.Interfaces;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(Stream fileStream, string fileName, CancellationToken cancellationToken);
}
