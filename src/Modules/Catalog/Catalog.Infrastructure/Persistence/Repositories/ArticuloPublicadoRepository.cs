using Catalog.Application.Common.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Configurations;

namespace Catalog.Infrastructure.Persistence.Repositories;

public class ArticuloPublicadoRepository : IArticuloPublicadoRepository
{
    private readonly CatalogDbContext _context;

    public ArticuloPublicadoRepository(CatalogDbContext context)
    {
        _context = context;
    }
    
    
    public async Task AddAsync(ArticuloPublicado articuloPublicado, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(articuloPublicado, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}