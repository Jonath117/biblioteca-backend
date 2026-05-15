using MediatR;
using Workflow.Application.Repositories;
using Workflow.Domain.Entities;

namespace Workflow.Application.Features.Assignments.AssignReviewer;

public class AssignReviewerCommandHandler : IRequestHandler<AssignReviewerCommand, Guid>
{
    private readonly IRevisionRepository _revisionRepository;

    public AssignReviewerCommandHandler(IRevisionRepository revisionRepository)
    {
        _revisionRepository = revisionRepository;
    }

    public async Task<Guid> Handle(AssignReviewerCommand request, CancellationToken cancellationToken)
    {
        var newRevision = new Revision
        {
            Id = Guid.NewGuid(),
            DocumentoId = request.DocumentId,
            AsesorId = request.AssessorId,
            Estado = "Pendiente",
            FechaAsignacion = DateTime.UtcNow
        };

        await _revisionRepository.AddAsync(newRevision, cancellationToken);

        return newRevision.Id;
    }
}