using Domain.Common;

namespace Domain
{
    public class Respuesta : BaseDomainModel
    {
        public string Descripcion { get; set; }
        public bool Correcta { get; set; }
        public int PreguntaId { get; set; }
        public virtual Pregunta? Pregunta { get; set; }
    }
}
