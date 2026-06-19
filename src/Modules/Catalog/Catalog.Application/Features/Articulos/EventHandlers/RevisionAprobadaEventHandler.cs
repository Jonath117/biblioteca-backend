using Catalog.Application.Common.Interfaces;
using Catalog.Domain.Entities;
using IAM.Application.Features.Usuarios.Queries;
using MediatR;
using Workflow.Application.Features.Revisions.EventHandlers;
using Workspace.Application.Features.ObtenerDocumentoPorId;

namespace Catalog.Application.Features.Articulos.EventHandlers;

public class RevisionAprobadaEventHandler : INotificationHandler<RevisionAprobadaEvent>
{
    private readonly ISender _sender;
    private readonly IArticuloPublicadoRepository _repository;
    private readonly ICatalogUnitOfWork _unitOfWork;
    
    public RevisionAprobadaEventHandler(
        ISender sender,
        IArticuloPublicadoRepository repository,
        ICatalogUnitOfWork unitOfWork)
    {
        _sender = sender;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RevisionAprobadaEvent notification, CancellationToken cancellationToken)
    {
        var queryDoc = new ObtenerDocumentoPorIdQuery(notification.DocumentoId);
        var resultDoc = await _sender.Send(queryDoc, cancellationToken);
        
        if (resultDoc.IsFailure || resultDoc.Value == null)
            throw new Exception($"No se pudo obtener el documento {notification.DocumentoId} de Workspace.");
        
        var documentoDto = resultDoc.Value;
        
        var autoresIds = new List<Guid> { documentoDto.AutorPrincipalId };
        
        if(documentoDto.CoautoresIds != null && documentoDto.CoautoresIds.Any())
            autoresIds.AddRange(documentoDto.CoautoresIds);

        var queryUsuarios = new ObtenerUsuariosPorIdsQuery(autoresIds);
        var resultUsuarios = await _sender.Send(queryUsuarios, cancellationToken);
        
        string cadenaNombresAutores = "Autores no especificados";

        if (resultUsuarios.IsSuccess && resultUsuarios.Value != null && resultUsuarios.Value.Any())
        {
            var nombresCompletos = resultUsuarios.Value.Select(u => $"{u.Nombre} {u.Apellido}".Trim());
            
            cadenaNombresAutores = string.Join(", ", nombresCompletos);
        }
        
        string carrera = "Ingeniería de Sistemas"; 
        string materia = "Proyecto de Grado";

        var articulo = ArticuloPublicado.Publicar(
            notification.RevisionId,
            documentoDto.Titulo,
            cadenaNombresAutores,
            documentoDto.Resumen,
            documentoDto.ArchivoUrl,
            carrera,
            materia
        );
        
        await _repository.AddAsync(articulo, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}