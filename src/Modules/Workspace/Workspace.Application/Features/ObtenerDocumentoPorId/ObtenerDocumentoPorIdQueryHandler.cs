using Workspace.Application.Common.Abstractions;
using Workspace.Application.Common.Interfaces;
using Workspace.Application.Common.Models;

namespace Workspace.Application.Features.ObtenerDocumentoPorId;

public class ObtenerDocumentoPorIdQueryHandler : IQueryHandler<ObtenerDocumentoPorIdQuery, DocumentoDto>
{
    private readonly IDocumentoRepository _repository;
    
    public ObtenerDocumentoPorIdQueryHandler(IDocumentoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<DocumentoDto>> Handle(ObtenerDocumentoPorIdQuery request,
        CancellationToken cancellationToken)
    {
        var documento = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (documento == null)
        {
            return Result.Failure<DocumentoDto>(new Error(
                "Workspace.DocumentoNoEncontrado",
                $"No se encontró ningún documento con el ID proporcionado: {request.Id}"));
        }
        
        var dto = new DocumentoDto(
            documento.Id,
            documento.AutorPrincipalId,
            documento.Titulo,
            documento.Resumen,
            documento.ArchivoUrl,
            documento.Estado,
            documento.FechaCreacion,
            documento.Coautores.Select(c => c.UsuarioId).ToList()
        );

        return Result.Success(dto);
    }
}