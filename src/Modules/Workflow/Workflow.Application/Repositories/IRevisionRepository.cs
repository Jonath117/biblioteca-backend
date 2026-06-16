using System;
using System.Threading;
using System.Threading.Tasks;
using Workflow.Domain.Entities;

namespace Workflow.Application.Repositories
{
    public interface IRevisionRepository
    {
        Task<Revision?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Revision?> GetByDocumentoIdAsync(Guid documentoId, CancellationToken cancellationToken = default);
        Task AddAsync(Revision revision, CancellationToken cancellationToken = default);
        Task UpdateAsync(Revision revision, CancellationToken cancellationToken = default);
    }
}