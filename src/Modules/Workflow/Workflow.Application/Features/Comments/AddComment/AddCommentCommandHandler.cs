using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Workflow.Application.Repositories;

namespace Workflow.Application.Features.Comments.AddComment
{
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
                throw new Exception($"Revisión con ID {request.RevisionId} no encontrada.");
            }

            revision.AgregarComentario(request.AutorId, request.Contenido);

            await _revisionRepository.UpdateAsync(revision, cancellationToken);

            return revision.Id;
        }
    }
}