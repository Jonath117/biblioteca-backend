using Workflow.Domain.Entities;

namespace Workflow.Application.Repositories;

public interface IRevisionRepository {
    Task AddAsync(Revision revision, CancellationToken cancellationToken);
    Task<Revision?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task UpdateAsync(Revision revision, CancellationToken cancellationToken);
}