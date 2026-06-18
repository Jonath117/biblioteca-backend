namespace Workspace.Application.Features.ObtenerDocumentoPorId;

public record DocumentoDto(Guid Id,
    Guid AutorPrincipalId,
    string Titulo,
    string Resumen,
    string ArchivoUrl,
    string Estado,
    DateTime FechaCreacion,
    List<Guid> CoautoresIds);