using System.ComponentModel;

namespace Mxss.Schedules.Enum
{
    public enum TypePerson
    {
        [Description("Familia")]
        Family = 1,
        [Description("Amigos")]
        Friendship = 2,
        [Description("Trabajo")]
        Work = 3,
        [Description("Servicios")]
        Services = 4
    }
}