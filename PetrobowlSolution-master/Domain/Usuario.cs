using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class Usuario : IdentityUser
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Telefono { get; set; }
        public bool IsActive { get; set; } = true;
        public int? Equipo { get; set; }
        public virtual ICollection<Persona>? Personas { get; set; }

    }
}
