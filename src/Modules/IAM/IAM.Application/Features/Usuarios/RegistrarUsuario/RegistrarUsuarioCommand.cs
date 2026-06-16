using MediatR;

namespace IAM.Application.Features.Usuarios.RegistrarUsuario;

public record RegistrarUsuarioCommand(
    string Email,
    string Nombre,
    string Apellido,
    string Password) : IRequest<Guid>;