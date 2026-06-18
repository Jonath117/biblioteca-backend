namespace Catalog.Domain.Entities;

public class Etiqueta
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;

    private readonly List<ArticuloEtiqueta> _articuloEtiquetas = new();
    public IReadOnlyCollection<ArticuloEtiqueta> ArticuloEtiquetas => _articuloEtiquetas.AsReadOnly();
    
    private Etiqueta() { }

    public static Etiqueta Crear(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la etiqueta no puede estar vacio");

        return new Etiqueta
        {
            Nombre = nombre.Trim().ToLower()
        };
    }
}