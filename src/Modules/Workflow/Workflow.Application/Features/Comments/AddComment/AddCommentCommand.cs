using MediatR;

namespace Workflow.Application.Features.Comments.AddComment;

public record AddCommentCommand(Guid RevisionId, string CommentContent, bool IsPublic) : IRequest<Guid>;