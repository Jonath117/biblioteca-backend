using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
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

    public async Task AddAsync(Revision revision, CancellationToken cancellationToken = default)
    {
        await _context.Set<Revision>().AddAsync(revision, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Revision?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Revision>()
            .Include(r => r.Comentarios)
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }

    public async Task<Revision?> GetByDocumentoIdAsync(Guid documentoId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Revision>()
            .Include(r => r.Comentarios)
            .FirstOrDefaultAsync(r => r.DocumentoId == documentoId, cancellationToken);
    }

    public async Task UpdateAsync(Revision revision, CancellationToken cancellationToken = default)
    {
        _context.Set<Revision>().Update(revision);
        await _context.SaveChangesAsync(cancellationToken);
    }
}