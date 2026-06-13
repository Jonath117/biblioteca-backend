using MediatR;

namespace IAM.Application.Features.Usuarios.LoginConGoogle;

public record AutenticarSsoCommand(string IdTokenGoogle): IRequest<string>;