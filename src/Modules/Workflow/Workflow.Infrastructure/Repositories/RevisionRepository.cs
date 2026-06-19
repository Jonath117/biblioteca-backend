using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Workflow.Application.Repositories;
using Workflow.Domain.Entities;
using Workflow.Infrastructure.Configurations;

namespace Workflow.Infrastructure.Repositories
{
    public class RevisionRepository : IRevisionRepository
    {
        private readonly WorkflowDbContext _context;

        public RevisionRepository(WorkflowDbContext context)
        {
            _context = context;
        }

        public async Task<Revision?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Revisiones
                .Include(r => r.Comentarios)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task AddAsync(Revision revision, CancellationToken cancellationToken = default)
        {
            await _context.Revisiones.AddAsync(revision, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Revision revision, CancellationToken cancellationToken = default)
        {
            _context.ChangeTracker.DetectChanges();


            foreach (var entry in _context.ChangeTracker.Entries<ComentarioRevision>())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.State = EntityState.Added;
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Revision>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Revisiones
                .Include(r => r.Comentarios)
                .ToListAsync(cancellationToken);
        }
    }
}