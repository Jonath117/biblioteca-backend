namespace Catalog.Domain.Entities;

public class ArticuloEtiqueta
{
    public Guid ArticuloId { get; internal set; }
    public int EtiquetaId { get; internal set; }
    
    public ArticuloPublicado Articulo { get; private set; } = null!;
    public Etiqueta Etiqueta { get; private set; } = null!;
    
    
    internal ArticuloEtiqueta() { }
}