using MediatR;
using Workflow.Application.Repositories;
using Workspace.Application.Common.Interfaces;
using Workspace.Application.Features.SubirDocumentoBorrador;
using Workflow.Domain.Entities;

namespace Workflow.Application.Features.Revisions.EventHandlers;

public class DocumentoSubidoEventHandler : INotificationHandler<DocumentoSubidoEvent>
{
    private readonly IRevisionRepository _revisionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DocumentoSubidoEventHandler(IRevisionRepository revisionRepository, IUnitOfWork unitOfWork)
    {
        _revisionRepository = revisionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DocumentoSubidoEvent notification, CancellationToken cancellationToken)
    {
        var revisionNueva = Revision.Crear(notification.DocumentoId);
        
        await _revisionRepository.AddAsync(revisionNueva, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}