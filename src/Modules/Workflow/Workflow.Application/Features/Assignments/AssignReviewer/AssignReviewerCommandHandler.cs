using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Workflow.Application.Repositories;

namespace Workflow.Application.Features.Assignments.AssignReviewer
{
    public class AssignReviewerCommandHandler : IRequestHandler<AssignReviewerCommand, bool>
    {
        private readonly IRevisionRepository _revisionRepository;

        public AssignReviewerCommandHandler(IRevisionRepository revisionRepository)
        {
            _revisionRepository = revisionRepository;
        }

        public async Task<bool> Handle(AssignReviewerCommand request, CancellationToken cancellationToken)
        {
            var revision = await _revisionRepository.GetByIdAsync(request.RevisionId, cancellationToken);
            
            if (revision == null)
            {
                throw new Exception($"No se encontró la revisión con ID {request.RevisionId}"); 
            }

            revision.AsignarRevisor(request.AsesorId);

            await _revisionRepository.UpdateAsync(revision, cancellationToken);
            
            return true;
        }
    }
}