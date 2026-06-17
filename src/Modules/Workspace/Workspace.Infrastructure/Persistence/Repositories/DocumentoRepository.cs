using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workspace.Application.Common.Interfaces;
using Workspace.Domain.Entities;
using Workspace.Infrastructure.Persistence.Configurations;

namespace Workspace.Infrastructure.Persistence.Repositories;

public class DocumentoRepository : IDocumentoRepository
{
    private readonly WorkspaceDbContext _dbContext;

    public DocumentoRepository(WorkspaceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Documento documento, CancellationToken cancellationToken)
    {
        await _dbContext.Documentos.AddAsync(documento, cancellationToken);
    }

    public async Task<Documento?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Documentos
            .Include(d => d.Coautores)
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
    }
}
