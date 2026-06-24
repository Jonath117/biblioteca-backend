using System.Threading;
using System.Threading.Tasks;
using IAM.Application.Events;
using MediatR;
using Workspace.Application.Common.Interfaces;
using Workspace.Domain.Entities;

namespace Workspace.Application.Features.Usuarios;

public class UsuarioCreadoEventHandler : INotificationHandler<UsuarioCreadoIntegrationEvent>
{
    private readonly IEstudianteReplicadoRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UsuarioCreadoEventHandler(IEstudianteReplicadoRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UsuarioCreadoIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var estudiante = new EstudianteReplicado
        {
            Id = notification.UsuarioId,
            Email = notification.Email,
            Nombre = notification.Nombre,
            Apellido = notification.Apellido
        };

        await _repository.AddAsync(estudiante, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
