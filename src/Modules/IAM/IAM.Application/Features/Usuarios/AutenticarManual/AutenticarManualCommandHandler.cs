using IAM.Application.Interfaces.Authentication;
using IAM.Application.Interfaces.Repository;
using IAM.Domain.Exceptions;
using MediatR;

namespace IAM.Application.Features.Usuarios.AutenticarManual;

public class AutenticarManualCommandHandler : IRequestHandler<AutenticarManualCommand, string>
{
    private readonly IUsuarioRepository _repository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AutenticarManualCommandHandler(IUsuarioRepository repository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _repository = repository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> Handle(AutenticarManualCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _repository.GetByEmailAsync(request.Email, cancellationToken);

        if (usuario == null)
            throw new DomainException("Credenciales incorrectas");
        
        if(string.IsNullOrEmpty(usuario.PasswordHash))
            throw new DomainException("Esta cuenta está vinculada a Google. Por favor, utiliza el botón de 'Iniciar sesión con Google'.");

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, usuario.PasswordHash);

        if (!isPasswordValid)
            throw new DomainException("Credenciales Incorrectas");

        if (!usuario.Activo)
            throw new DomainException("Esta cuenta ha sido desactivada por un Admin");
        
        return _jwtTokenGenerator.GenerateToken(usuario);
    }
}