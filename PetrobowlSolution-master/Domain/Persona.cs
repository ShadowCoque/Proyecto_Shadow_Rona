using Domain.Common;

namespace Domain
{
    public class Persona : BaseDomainModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public bool Capitan { get; set; }
        public string Email { get; set; }
        public UserStatus Status { get; set; }
        public int? EquipoId { get; set; }
        public virtual Equipo? Equipo { get; set; }
        public string? UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
