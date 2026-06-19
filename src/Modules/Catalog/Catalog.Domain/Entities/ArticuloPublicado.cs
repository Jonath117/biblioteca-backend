namespace Catalog.Domain.Entities;

public class ArticuloPublicado
{
    public Guid Id { get; private set; }
    public Guid RevisionId { get; private set; }
    public string Titulo { get; private set; } = string.Empty;
    public string NombresAutores { get; private set; } = string.Empty;
    public string Resumen { get; private set; } = string.Empty;
    public string ArchivoUrl { get; private set; } = string.Empty;
    public string Carrera { get; private set; } = string.Empty;
    public string Materia { get; private set; } = string.Empty;
    public DateTime FechaPublicacion { get; private set; }

    private readonly List<ArticuloEtiqueta> _articulosEtiquetas = new();
    public IReadOnlyCollection<ArticuloEtiqueta> ArticuloEtiquetas => _articulosEtiquetas.AsReadOnly();
    
    private ArticuloPublicado() { }

    public static ArticuloPublicado Publicar(
        Guid revisionId,
        string titulo,
        string nombresAutores,
        string resumen,
        string archivoUrl,
        string carrera,
        string materia
    )
    {
        return new ArticuloPublicado
        {
            Id = Guid.NewGuid(),
            RevisionId = revisionId,
            Titulo = titulo,
            NombresAutores = nombresAutores,
            Resumen = resumen,
            ArchivoUrl = archivoUrl,
            Carrera = carrera,
            Materia = materia,
            FechaPublicacion = DateTime.UtcNow
        };
    }

    public void AgregarEtiqueta(int etiquetaId)
    {
        if(_articulosEtiquetas.Any(e => e.EtiquetaId == etiquetaId)) return;
        
        _articulosEtiquetas.Add(new ArticuloEtiqueta{ArticuloId = this.Id, EtiquetaId = etiquetaId});
    }
}