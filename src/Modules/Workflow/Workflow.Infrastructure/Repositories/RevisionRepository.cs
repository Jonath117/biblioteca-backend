using Microsoft.EntityFrameworkCore;
using Workflow.Application.Repositories;
using Workflow.Domain.Entities;
using Workflow.Infrastructure.Configurations;

namespace Workflow.Infrastructure.Repositories;

public class RevisionRepository : IRevisionRepository
{
    private readonly WorkflowDbContext _context;

    public RevisionRepository(WorkflowDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Revision revision, CancellationToken cancellationToken)
    {
        await _context.Set<Revision>().AddAsync(revision, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Revision?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Set<Revision>()
            .Include(r => r.Comentarios)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Revision revision, CancellationToken cancellationToken)
    {
        _context.Set<Revision>().Update(revision);
        await _context.SaveChangesAsync(cancellationToken);
    }
}