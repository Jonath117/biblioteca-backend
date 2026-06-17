using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Workflow.Domain.Entities
{
    public class Revision
    {
        public Guid Id { get; private set; }
        public Guid DocumentoId { get; private set; }
        public Guid? AsesorId { get; private set; }
        
        [Column(TypeName = "integer")]
        public EstadoRevision Estado { get; private set; }
        
        public DateTime FechaAsignacion { get; private set; }
        public DateTime? FechaResolucion { get; private set; }
        
        private readonly List<ComentarioRevision> _comentarios = new();
        public IReadOnlyCollection<ComentarioRevision> Comentarios => _comentarios.AsReadOnly();

        private Revision() { }

        public static Revision Crear(Guid documentoId)
        {
            return new Revision
            {
                Id = Guid.NewGuid(),
                DocumentoId = documentoId,
                Estado = EstadoRevision.Pendiente,
                FechaAsignacion = DateTime.UtcNow
            };
        }

        public void AsignarRevisor(Guid asesorId)
        {
            if (Estado != EstadoRevision.Pendiente)
                throw new InvalidOperationException("Solo se pueden asignar revisores a documentos pendientes.");

            AsesorId = asesorId;
            Estado = EstadoRevision.EnRevision;
        }

        public void AgregarComentario(Guid autorComentarioId, string contenido)
        {
            if (Estado != EstadoRevision.EnRevision)
                throw new InvalidOperationException("Solo se pueden agregar comentarios durante la revisión.");

            var comentario = ComentarioRevision.Crear(this.Id, autorComentarioId, contenido);
            _comentarios.Add(comentario);
        }

        public void Resolver(EstadoRevision nuevoEstado)
        {
            if (nuevoEstado == EstadoRevision.Pendiente || nuevoEstado == EstadoRevision.EnRevision)
                throw new ArgumentException("Estado de resolución inválido.");

            Estado = nuevoEstado;
            FechaResolucion = DateTime.UtcNow;
            
        }
    }

    public enum EstadoRevision
    {
        Pendiente,
        EnRevision,
        Aprobado,
        Rechazado,
        RequiereCorreccion
    }
}