using IAM.Application.Interfaces.Repository;
using IAM.Domain.Entities;
using IAM.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IAM.Infrastructure.Persistence.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly IamDbContext _context;

    public UsuarioRepository(IamDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken)
    {
        return _context.Usuarios.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task AddAsync(Usuario usuario, CancellationToken cancellationToken)
    {
        await _context.Usuarios.AddAsync(usuario, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }
}