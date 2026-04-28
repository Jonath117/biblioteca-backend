namespace IAM.Domain.Entities;

public class Rol
{
    public int Id { get; set; }
    public String Nombre { get; set; }

    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}