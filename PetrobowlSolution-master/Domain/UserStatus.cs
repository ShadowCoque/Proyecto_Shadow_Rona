using System.Runtime.Serialization;

namespace Domain
{
    public enum UserStatus
    {
        [EnumMember(Value = "Usuario Inactivo")]
        Inactivo,
        [EnumMember(Value = "Usuario Activo")]
        Activo
    }
}
