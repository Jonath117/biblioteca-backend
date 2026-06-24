using IAM.Application.Interfaces.Repository;
using MediatR;
using IAM.Domain.Exceptions;
using IAM.Domain.Entities;

namespace IAM.Application.Features.Usuarios.RegistrarUsuario;

public class RegistrarUsuarioCommandHandler : IRequestHandler<RegistrarUsuarioCommand, Guid>
{
    private readonly IUsuarioRepository _repository;
    private readonly IPublisher _publisher;

    private const int ROL_ESTUDIANTE = 1;
    
    public RegistrarUsuarioCommandHandler(IUsuarioRepository repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(RegistrarUsuarioCommand request, CancellationToken cancellationToken)
    {
        var existeEmail = await _repository.ExisteEmailAsync(request.Email, cancellationToken);
        if (existeEmail)
        {
            throw new DomainException($"El correo {request.Email} ya se encuentra registrado.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var nuevoUsuario = Usuario.CreateManual(
            request.Email, request.Nombre, request.Apellido, passwordHash, ROL_ESTUDIANTE);
        
        await _repository.AddAsync(nuevoUsuario, cancellationToken);
        await _repository.SaveAsync(cancellationToken);
        
        await _publisher.Publish(new IAM.Application.Events.UsuarioCreadoIntegrationEvent(
            nuevoUsuario.Id, 
            nuevoUsuario.Email, 
            nuevoUsuario.Nombre, 
            nuevoUsuario.Apellido), cancellationToken);
            
        return nuevoUsuario.Id;
    }
}