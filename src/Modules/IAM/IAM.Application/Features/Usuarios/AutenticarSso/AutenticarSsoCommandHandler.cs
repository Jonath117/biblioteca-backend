using Google.Apis.Auth;
using IAM.Application.Interfaces.Authentication;
using IAM.Application.Interfaces.Repository;
using IAM.Domain.Entities;
using IAM.Domain.Exceptions;
using MediatR;

namespace IAM.Application.Features.Usuarios.AutenticarSso;

public class AutenticarSsoCommandHandler : IRequestHandler<AutenticarSsoCommand, string>
{
    private readonly IUsuarioRepository _usuarioRepository;
    private const int ROL_ESTUDIANTE_ID = 1;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    
    public AutenticarSsoCommandHandler(IUsuarioRepository usuarioRepository, IJwtTokenGenerator jwtTokenGenerator)
    {
        _usuarioRepository = usuarioRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<string> Handle(AutenticarSsoCommand request, CancellationToken cancellationToken)
    {
        GoogleJsonWebSignature.Payload payload;

        try
        {
            payload = await GoogleJsonWebSignature.ValidateAsync(request.IdTokenGoogle);
        }
        catch (InvalidJwtException)
        {
            throw new DomainException("El token de google es invalido o ha expirado");
        }

        if (!payload.Email.EndsWith("ucb.edu.bo", StringComparison.OrdinalIgnoreCase))
        {
            throw new DomainException("Solo se permiten correos institucionales de la UCB");
        }
        
        var usuario = await _usuarioRepository.GetByEmailAsync(payload.Email, cancellationToken);

        if (usuario == null)
        {
            usuario = Usuario.CreateSSO(
                payload.Email,
                payload.GivenName,
                payload.FamilyName,
                ROL_ESTUDIANTE_ID
            );
            
            await _usuarioRepository.AddAsync(usuario, cancellationToken);
            await _usuarioRepository.SaveAsync(cancellationToken);
        }

        if (!usuario.Activo)
        {
            throw new DomainException("Esta cuenta ha sido desactivada");
        }

        string miTokenInterno = $"AQUI_VA_TU_TOKEN_JWT_CON_ROL_{usuario.RolId}";
        return _jwtTokenGenerator.GenerateToken(usuario);
    }
}