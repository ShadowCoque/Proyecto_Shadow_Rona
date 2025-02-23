using Domain.Common;

namespace Domain
{
    public class Resultado : BaseDomainModel
    {
        public int Total { get; set; }
        public int EquipoId { get; set; }
        public virtual  Equipo Equipos { get; set; }
        public int CompetenciaId { get; set; }
        public virtual  Competencia Competencias { get; set; }
    }
}
