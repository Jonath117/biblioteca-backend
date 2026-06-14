using IAM.Domain.Entities;

namespace IAM.Application.Interfaces.Repository;

public interface IUsuarioRepository
{
    Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(Usuario usuario, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
    Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken);
}