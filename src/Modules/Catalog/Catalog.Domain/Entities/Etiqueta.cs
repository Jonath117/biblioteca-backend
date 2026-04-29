namespace Catalog.Domain.Entities;

public class Etiqueta
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;

    public ICollection<ArticuloEtiqueta> ArticuloEtiquetas { get; set; } = new List<ArticuloEtiqueta>();
}