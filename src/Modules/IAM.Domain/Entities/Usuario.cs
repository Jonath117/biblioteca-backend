namespace IAM.Domain.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string NombreCompleto  { get; set; } = string.Empty;
    public int RolId { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime FechaRegistro { get; set; }

    public Rol Rol { get; set; } = null;
}