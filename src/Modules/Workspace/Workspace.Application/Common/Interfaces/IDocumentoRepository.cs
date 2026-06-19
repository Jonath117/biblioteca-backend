using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Workspace.Domain.Entities;

namespace Workspace.Application.Common.Interfaces;

public interface IDocumentoRepository
{
    Task AddAsync(Documento documento, CancellationToken cancellationToken);
    Task<Documento?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Documento>> GetByAutorPrincipalIdAsync(Guid autorPrincipalId, CancellationToken cancellationToken);
}
