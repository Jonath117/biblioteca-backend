namespace Catalog.Application.Common.Interfaces;

public interface ICatalogUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}