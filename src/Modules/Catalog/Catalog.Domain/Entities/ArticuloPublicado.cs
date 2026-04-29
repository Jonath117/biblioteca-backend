namespace Catalog.Domain.Entities;

public class ArticuloPublicado
{
    public Guid Id { get; set; }
    public Guid RevisionId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string NombresAutores { get; set; } = string.Empty;
    public string Resumen { get; set; } = string.Empty;
    public string ArchivoUrl { get; set; } = string.Empty;
    public string Carrera { get; set; } = string.Empty;
    public string Materia { get; set; } = string.Empty;
    public DateTime FechaPublicacion { get; set; }

    public ICollection<ArticuloEtiqueta> ArticuloEtiquetas { get; set; } = new List<ArticuloEtiqueta>();
}