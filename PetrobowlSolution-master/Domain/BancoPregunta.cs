using Domain.Common;

namespace Domain
{
    public class BancoPregunta : BaseDomainModel
    {
        public string? Periodo { get; set; }
        public int CompetenciaId { get; set; }
        public virtual ICollection<Pregunta>? Preguntas { get; set; }
        public virtual Competencia? Competencia { get; set; }
    }
}
