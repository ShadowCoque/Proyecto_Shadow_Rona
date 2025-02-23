using System.Runtime.Serialization;

namespace Domain
{
    public enum PreguntaStatus
    {
        [EnumMember(Value = "Pregunta Inactiva")]
        Inactiva,
        [EnumMember(Value = "Pregunta Activa")]
        Activa
    }
}
