using Domain.Common;

namespace Domain
{
    public class Equipo : BaseDomainModel
    {
        public string Nombre { get; set; }
        public string Universidad { get; set; }
        public int CompetenciaId { get; set; }
        public string? Grupo { get; set; }
        public virtual Competencia? Competencia { get; set; }
        public virtual ICollection<Persona>? Personas { get; set; }
        public virtual ICollection<Resultado>? Resultados { get; set; }

    }
}
