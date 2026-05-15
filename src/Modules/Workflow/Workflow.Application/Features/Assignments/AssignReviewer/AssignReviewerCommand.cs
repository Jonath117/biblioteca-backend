using MediatR;

namespace Workflow.Application.Features.Assignments.AssignReviewer;

public record AssignReviewerCommand(Guid DocumentId, Guid AssessorId) : IRequest<Guid>;