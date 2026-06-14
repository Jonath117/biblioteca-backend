using MediatR;

namespace IAM.Application.Features.Usuarios.AutenticarManual;

public record AutenticarManualCommand(
    string Email,
    string Password) : IRequest<string>;