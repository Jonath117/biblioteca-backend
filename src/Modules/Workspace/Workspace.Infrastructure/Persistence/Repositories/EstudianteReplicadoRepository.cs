using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Workspace.Application.Common.Interfaces;
using Workspace.Domain.Entities;
using Workspace.Infrastructure.Persistence.Configurations;

namespace Workspace.Infrastructure.Persistence.Repositories;

public class EstudianteReplicadoRepository : IEstudianteReplicadoRepository
{
    private readonly WorkspaceDbContext _context;

    public EstudianteReplicadoRepository(WorkspaceDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(EstudianteReplicado estudiante, CancellationToken cancellationToken)
    {
        await _context.EstudiantesReplicados.AddAsync(estudiante, cancellationToken);
    }

    public async Task<EstudianteReplicado?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.EstudiantesReplicados
            .FirstOrDefaultAsync(e => e.Email == email, cancellationToken);
    }
}
