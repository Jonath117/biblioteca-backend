namespace Workflow.Domain.Entities;

public class Revision
{
    public Guid Id { get; set; }
    public Guid DocumentoId { get; set; }
    public Guid AsesorId { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public DateTime FechaAsignacion { get; set; }
    public DateTime? FechaResolucion { get; set; }

    public ICollection<ComentarioRevision> Comentarios { get; set; } = new List<ComentarioRevision>();
}