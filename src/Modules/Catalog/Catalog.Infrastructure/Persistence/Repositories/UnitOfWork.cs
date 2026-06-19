using Catalog.Application.Common.Interfaces;
using Catalog.Infrastructure.Persistence.Configurations;

namespace Catalog.Infrastructure.Persistence.Repositories;

public class CatalogUnitOfWork : ICatalogUnitOfWork
{
    private readonly CatalogDbContext _context;

    public CatalogUnitOfWork(CatalogDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken); 
    }
}