using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Workflow.Domain.Entities;

namespace Workflow.Application.Repositories
{
    public interface IRevisionRepository
    {
        Task<Revision?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(Revision revision, CancellationToken cancellationToken = default);
        Task UpdateAsync(Revision revision, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Revision>> GetAllAsync(CancellationToken cancellationToken = default); // <-- Nuevo método
    }
}