namespace IAM.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string Nombre { get; private set; } = string.Empty;
    public string Apellido { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public int RolId { get; private set; }
    public bool Activo { get; private set; } = true;
    public DateTime FechaRegistro { get; private set; }

    public Rol Rol { get; private set; } = null!;

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

    public void ActulizarNombre(string nuevoNombre)
    {
        if(string.IsNullOrWhiteSpace(nuevoNombre))
            throw new ArgumentException("El nombre es obligatorio", nameof(nuevoNombre));

        if (nuevoNombre == Nombre)
            throw new ArgumentException("El nuevo nombre es igual al actual");
        
        Nombre = nuevoNombre;
    }

    public void ActulizarApellido(string nuevoApellido)
    {
        if (string.IsNullOrWhiteSpace(nuevoApellido))
            throw new ArgumentException("El apellido es  obligatorio", nameof(nuevoApellido));
        
        if (nuevoApellido == Apellido)
            throw new ArgumentException("El nuevo apellido es igual al actual");
        
        Apellido = nuevoApellido;
    }

    public void AsignarRol(int nuevoRolId)
    {
        if(nuevoRolId <= 0)
            throw new ArgumentOutOfRangeException("Rol inavlido");
        
        RolId = nuevoRolId;
    }

    public void ActivarCuenta()
    {
        if(Activo)
            throw new ArgumentException("La cuenta ya esta activa");

        Activo = true;
    }

    public void DesactivarCuenta()
    {
        if(!Activo)
            throw new ArgumentException("La cuenta ya esta desactiva");
        
        Activo = false;
    }
    
}