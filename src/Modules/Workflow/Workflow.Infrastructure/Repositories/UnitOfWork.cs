using Workflow.Application.Interfaces;
using Workflow.Infrastructure.Configurations;

namespace Workflow.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly WorkflowDbContext _context;

    public UnitOfWork(WorkflowDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken =  default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}