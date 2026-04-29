namespace Workflow.Domain.Entities;

public class ComentarioRevision
{
    public Guid Id { get; set; }
    public Guid RevisionId { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public bool EsPublico { get; set; }
    public DateTime Fecha { get; set; }

    public Revision Revision { get; set; } = null;
}