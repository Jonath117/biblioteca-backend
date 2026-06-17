using MediatR;
using System;

namespace Workflow.Application.Features.Comments.AddComment
{
    public record AddCommentCommand(Guid RevisionId, Guid AutorId, string Contenido) : IRequest<Guid>;
}