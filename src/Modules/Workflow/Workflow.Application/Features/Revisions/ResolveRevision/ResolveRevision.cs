using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Workflow.Application.Repositories;
using Workflow.Domain.Entities;

namespace Workflow.Application.Features.Revisions.ResolveRevision
{
    public record ResolveRevisionCommand(Guid RevisionId, EstadoRevision NuevoEstado) : IRequest;

    public class ResolveRevisionCommandHandler : IRequestHandler<ResolveRevisionCommand>
    {
        private readonly IRevisionRepository _repository;

        public ResolveRevisionCommandHandler(IRevisionRepository repository)
        {
            _repository = repository;
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
        }
    }
}