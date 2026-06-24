using System;
using MediatR;

namespace IAM.Application.Events;

public class UsuarioCreadoIntegrationEvent : INotification
{
    public Guid UsuarioId { get; }
    public string Email { get; }
    public string Nombre { get; }
    public string Apellido { get; }

    public UsuarioCreadoIntegrationEvent(Guid usuarioId, string email, string nombre, string apellido)
    {
        UsuarioId = usuarioId;
        Email = email;
        Nombre = nombre;
        Apellido = apellido;
    }
}
