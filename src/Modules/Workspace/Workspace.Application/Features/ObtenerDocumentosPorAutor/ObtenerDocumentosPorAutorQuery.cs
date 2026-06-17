using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workspace.Application.Common.Abstractions;
using Workspace.Application.Common.Interfaces;
using Workspace.Application.Common.Models;

namespace Workspace.Application.Features.ObtenerDocumentosPorAutor;

public record DocumentoDto(
    Guid Id,
    Guid AutorPrincipalId,
    string Titulo,
    string Resumen,
    string ArchivoUrl,
    string Estado,
    DateTime FechaCreacion,
    DateTime FechaModificacion,
    List<Guid> CoautoresIds
);

public record ObtenerDocumentosPorAutorQuery(Guid AutorPrincipalId) : IQuery<List<DocumentoDto>>;

public class ObtenerDocumentosPorAutorQueryHandler : IQueryHandler<ObtenerDocumentosPorAutorQuery, List<DocumentoDto>>
{
    private readonly IDocumentoRepository _repository;

    public ObtenerDocumentosPorAutorQueryHandler(IDocumentoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<DocumentoDto>>> Handle(ObtenerDocumentosPorAutorQuery request, CancellationToken cancellationToken)
    {
        var documentos = await _repository.GetByAutorPrincipalIdAsync(request.AutorPrincipalId, cancellationToken);

        var dtos = documentos.Select(d => new DocumentoDto(
            d.Id,
            d.AutorPrincipalId,
            d.Titulo,
            d.Resumen,
            d.ArchivoUrl,
            d.Estado,
            d.FechaCreacion,
            d.FechaModificacion,
            d.Coautores.Select(c => c.UsuarioId).ToList()
        )).ToList();

        return Result.Success(dtos);
    }
}
