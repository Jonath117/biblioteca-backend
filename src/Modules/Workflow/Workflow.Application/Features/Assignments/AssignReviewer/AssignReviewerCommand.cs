using MediatR;
using System;

namespace Workflow.Application.Features.Assignments.AssignReviewer
{
    public record AssignReviewerCommand(Guid RevisionId, Guid AsesorId) : IRequest<bool>;
}