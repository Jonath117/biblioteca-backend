using Catalog.Domain.Entities;

namespace Catalog.Application.Common.Interfaces;

public interface IArticuloPublicadoRepository
{
    Task AddAsync(ArticuloPublicado articuloPublicado, CancellationToken cancellationToken = default);
}