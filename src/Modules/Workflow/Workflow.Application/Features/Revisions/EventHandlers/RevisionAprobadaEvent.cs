using MediatR;

namespace Workflow.Application.Features.Revisions.EventHandlers;

/// <summary>
/// Evento que se dispara cuando un Asesor o Admin aprueba una revisión.
/// El modulo Catalog escuchara esto para publicar el articulo.
/// no tocar mucho
/// </summary>
public record RevisionAprobadaEvent(
    Guid RevisionId, 
    Guid DocumentoId
) : INotification;