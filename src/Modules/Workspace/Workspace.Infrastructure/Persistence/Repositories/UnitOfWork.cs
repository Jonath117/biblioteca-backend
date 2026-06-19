using System.Threading;
using System.Threading.Tasks;
using Workspace.Application.Common.Interfaces;
using Workspace.Infrastructure.Persistence.Configurations;

namespace Workspace.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly WorkspaceDbContext _dbContext;

    public UnitOfWork(WorkspaceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
