namespace IAM.Domain.Entities;

public class Usuario
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public string Apellido { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public int RolId { get; set; }
    public bool Activo { get; set; } = true;
    public DateTime FechaRegistro { get; set; }

    public Rol Rol { get; set; } = null!;

    private Usuario(){ }

    private Usuario(string email, string nombre, string apellido, string passwordHash, int rolId)
    {
        Id = Guid.NewGuid();
        Email = email;
        Nombre = nombre;
        Apellido = apellido;
        PasswordHash = passwordHash;
        RolId = rolId;
        Activo = true;
        FechaRegistro = DateTime.UtcNow;
    }

    public static Usuario Create(string email, string nombre, string apellido, string passwordHash, int rolId)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Trim().EndsWith("@ucb.edu.bo"))
            throw new ArgumentException("El email debe pertenecer al dominio institucional @ucb.edu.bo");
        
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre es obligatorio", nameof(nombre));
        
        if (string.IsNullOrWhiteSpace(apellido))
            throw new ArgumentException("El apellido es obligatorio", nameof(apellido));
        
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("La contrasena no puede estar vacio.");

        return new Usuario(email.Trim().ToLowerInvariant(), nombre.Trim(), apellido.Trim(), passwordHash, rolId);
    }
    
    
}