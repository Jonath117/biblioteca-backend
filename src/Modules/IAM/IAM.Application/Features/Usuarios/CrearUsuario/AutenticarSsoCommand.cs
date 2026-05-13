using MediatR;

namespace IAM.Application.Features.Usuarios.CrearUsuario;

public record AutenticarSsoCommand(string IdTokenGoogle): IRequest<string>;