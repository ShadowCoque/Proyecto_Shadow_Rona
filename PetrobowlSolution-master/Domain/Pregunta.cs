using Domain.Common;

namespace Domain
{
    public class Pregunta : BaseDomainModel
    {
        public string? Descripcion { get; set; }
        public int BancoPreguntaId { get; set; }
        public PreguntaStatus Status { get; set; } = PreguntaStatus.Activa;
        public virtual ICollection<Respuesta>? Respuestas { get; set; }
        public virtual BancoPregunta? BancoPregunta { get; set; }

    }
}
