using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Workflow.Application.Repositories;

namespace Workflow.Application.Features.Revisions.GetAll
{
    public record ComentarioDto(Guid Id, Guid AutorId, string Contenido, DateTime FechaCreacion);

    public record RevisionDto(
        Guid Id,
        Guid DocumentoId,
        Guid? AsesorId,
        string Estado,
        DateTime FechaAsignacion,
        DateTime? FechaResolucion,
        List<ComentarioDto> Comentarios
    );

    public record GetAllRevisionesQuery() : IRequest<List<RevisionDto>>;

    public class GetAllRevisionesQueryHandler : IRequestHandler<GetAllRevisionesQuery, List<RevisionDto>>
    {
        private readonly IRevisionRepository _repository;

        public GetAllRevisionesQueryHandler(IRevisionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RevisionDto>> Handle(GetAllRevisionesQuery request, CancellationToken cancellationToken)
        {
            var revisiones = await _repository.GetAllAsync(cancellationToken);

            return revisiones.Select(r => new RevisionDto(
                r.Id,
                r.DocumentoId,
                r.AsesorId,
                r.Estado.ToString(),
                r.FechaAsignacion,
                r.FechaResolucion,
                r.Comentarios.Select(c => new ComentarioDto(
                    c.Id, 
                    c.AutorId, 
                    c.Contenido, 
                    c.FechaCreacion
                )).OrderByDescending(c => c.FechaCreacion).ToList()
            )).ToList();
        }
    }
}