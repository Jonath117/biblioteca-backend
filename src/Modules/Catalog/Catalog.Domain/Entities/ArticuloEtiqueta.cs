namespace Catalog.Domain.Entities;

public class ArticuloEtiqueta
{
    public Guid ArticuloId { get; set; }
    public int EtiquetaId { get; set; }
    
    public ArticuloPublicado Articulo { get; set; } = null!;
    public Etiqueta Etiqueta { get; set; } = null!;
}