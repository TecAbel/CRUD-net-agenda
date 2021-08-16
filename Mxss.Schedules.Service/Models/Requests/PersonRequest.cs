using Mxss.Schedules.Enum;

namespace Mxss.Schedules.Service.Models.Requests
{
    public class PersonRequest
    {
        /// <summary>
        /// Nombre de la persona
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Correo electrónico de la persona
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Número de la persona
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Tipo de persona: 1 = Familia, 2 = Amigos, 3 = Trabajo, 4 = Servicios
        /// </summary>
        public TypePerson? TypePerson { get; set; }
    }
}