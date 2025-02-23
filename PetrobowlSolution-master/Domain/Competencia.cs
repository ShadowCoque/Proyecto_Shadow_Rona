using Domain.Common;

namespace Domain
{
    public class Competencia : BaseDomainModel
    {
        public int Anio { get; set; }
        public virtual ICollection<BancoPregunta>? BancoPreguntas { get; set; }
        public virtual ICollection<Equipo>? Equipos { get; set; }
        public virtual ICollection<Resultado>? Resultados { get; set; }
    }
}
