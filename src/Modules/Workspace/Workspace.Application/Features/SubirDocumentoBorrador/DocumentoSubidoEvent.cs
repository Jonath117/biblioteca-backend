using MediatR;

namespace Workspace.Application.Features.SubirDocumentoBorrador;

public record DocumentoSubidoEvent(
    Guid DocumentoId,
    Guid AutorPrincipalId,
    string Titulo) : INotification;