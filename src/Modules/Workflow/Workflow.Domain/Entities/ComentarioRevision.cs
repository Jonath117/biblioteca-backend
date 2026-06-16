using System;

namespace Workflow.Domain.Entities
{
    public class ComentarioRevision
    {
        public Guid Id { get; private set; }
        public Guid RevisionId { get; private set; }
        public Guid AutorId { get; private set; }
        public string Contenido { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        private ComentarioRevision() { }

        internal static ComentarioRevision Crear(Guid revisionId, Guid autorId, string contenido)
        {
            if (string.IsNullOrWhiteSpace(contenido))
                throw new ArgumentException("El comentario no puede estar vacío.");

            return new ComentarioRevision
            {
                Id = Guid.NewGuid(),
                RevisionId = revisionId,
                AutorId = autorId,
                Contenido = contenido,
                FechaCreacion = DateTime.UtcNow
            };
        }
    }
}