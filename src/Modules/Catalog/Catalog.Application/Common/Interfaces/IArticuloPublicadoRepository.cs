using Catalog.Application.Features.Articulos.Queries.ObtenerArticulosPublicados;
using Catalog.Domain.Entities;

namespace Catalog.Application.Common.Interfaces;

public interface IArticuloPublicadoRepository
{
    Task AddAsync(ArticuloPublicado articuloPublicado, CancellationToken cancellationToken = default);
    Task<List<ArticuloPublicadoDto>> ObtenerPublicadosAsync(CancellationToken cancellationToken = default);
}