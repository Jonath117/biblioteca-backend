using System;
using System.Threading;
using System.Threading.Tasks;
using Workspace.Domain.Entities;

namespace Workspace.Application.Common.Interfaces;

public interface IEstudianteReplicadoRepository
{
    Task AddAsync(EstudianteReplicado estudiante, CancellationToken cancellationToken);
    Task<EstudianteReplicado?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}
