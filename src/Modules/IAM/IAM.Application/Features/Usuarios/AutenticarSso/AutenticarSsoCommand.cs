using MediatR;

namespace IAM.Application.Features.Usuarios.AutenticarSso;

public record AutenticarSsoCommand(string IdTokenGoogle): IRequest<string>;