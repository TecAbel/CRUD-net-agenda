using System.ComponentModel.DataAnnotations;

namespace Mxss.Schedules.Service.Models.Requests
{
    public class NewPersonRequest
    {
        [Required(ErrorMessage = "El tipo de persona es requerido")]
        public int PersonType { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "El teléfono es requerido")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El número de teléfono debe contener 10 caracteres")]
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "El email que intenta registrar no es válido")]
        public string Email { get; set; }
    }
}