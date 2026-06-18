using MediatR;
using Workflow.Application.Features.Revisions.EventHandlers;
using Workflow.Application.Interfaces;
using Workflow.Application.Repositories;
using Workflow.Domain.Entities;

namespace Workflow.Application.Features.Revisions.ResolveRevision
{
    public record ResolveRevisionCommand(Guid RevisionId, EstadoRevision NuevoEstado) : IRequest;

    public class ResolveRevisionCommandHandler : IRequestHandler<ResolveRevisionCommand>
    {
        private readonly IRevisionRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPublisher _publisher;

        public ResolveRevisionCommandHandler(IRevisionRepository repository, IUnitOfWork unitOfWork, IPublisher publisher)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _publisher = publisher;
        }

        public async Task Handle(ResolveRevisionCommand request, CancellationToken cancellationToken)
        {
            var revision = await _repository.GetByIdAsync(request.RevisionId, cancellationToken);
            if (revision == null)
            {
                throw new Exception($"Revision con ID {request.RevisionId} no encontrada.");
            }

            revision.Resolver(request.NuevoEstado);
            
            await _repository.UpdateAsync(revision, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            if (request.NuevoEstado == EstadoRevision.Aprobado)
            {
                var evento = new RevisionAprobadaEvent(revision.Id, revision.DocumentoId);
                await _publisher.Publish(evento, cancellationToken);
            }
        }
    }
}