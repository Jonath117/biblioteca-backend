namespace Workspace.Domain.Entities;

public class Documento
{
    public Guid Id { get; set; }
    public Guid AutorPrincipalId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Resumen { get; set; } = string.Empty;
    public string ArchivoUrl { get; set; } = string.Empty;
    public string Estado { get; set; } = "Borrador";
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
    
    public ICollection<Coautor> Coautores { get; set; } = new List<Coautor>();
}

