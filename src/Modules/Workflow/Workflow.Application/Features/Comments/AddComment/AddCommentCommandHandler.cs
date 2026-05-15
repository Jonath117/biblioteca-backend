using MediatR;
using Workflow.Application.Repositories;
using Workflow.Domain.Entities;

namespace Workflow.Application.Features.Comments.AddComment;

public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand, Guid>
{
    private readonly IRevisionRepository _revisionRepository;

    public AddCommentCommandHandler(IRevisionRepository revisionRepository)
    {
        _revisionRepository = revisionRepository;
    }

    public async Task<Guid> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        var revision = await _revisionRepository.GetByIdAsync(request.RevisionId, cancellationToken);
        
        if (revision == null)
        {
            throw new Exception("No se encontró la revisión especificada.");
        }

        var newComment = new ComentarioRevision
        {
            Id = Guid.NewGuid(),
            RevisionId = revision.Id,
            Comentario = request.CommentContent,
            EsPublico = request.IsPublic,
            Fecha = DateTime.UtcNow
        };


        if (revision.Estado == "Pendiente")
        {
            revision.Estado = "En Revisión";
        }

        revision.Comentarios.Add(newComment);

        await _revisionRepository.UpdateAsync(revision, cancellationToken);

        return newComment.Id;
    }
}