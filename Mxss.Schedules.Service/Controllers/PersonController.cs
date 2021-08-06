using Microsoft.AspNetCore.Mvc;
using Mxss.Schedules.Service.Extensions;
using Mxss.Schedules.Service.Models.Requests;
using Mxss.Schedules.Service.Models.Responses;
using Mxss.Schedules.Service.Models.Responses.Person;
using Mxss.Schedules.Service.Services.Person;

namespace Mxss.Schedules.Service.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PersonController : ControllerBase
    {

        private readonly IPersonManagement _personManagement;

        public PersonController(IPersonManagement personManagement)
        {
            _personManagement = personManagement;
        }

        /// <summary>
        /// Obtiene el listado de personas registradas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[Action]")]
        public ActionResult<PersonDetailListResponse> Retrieve()
        {
            var res = _personManagement.Retrieve();
            return new PersonDetailListResponse()
            {
                Data = res,
                Message = res.Count > 0 ? 
                    "Personas registradas obtenidas correctamente" :
                    "Aún no hay personas registradas en la agenda"
            };
        }

        /// <summary>
        /// Registra una nueva persona en la agenda
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [CatchException("Ha ocurrido un error al intentar registrar un nuevo contacto")]
        public ActionResult<ActionResponse> NewPerson(NewPersonRequest request)
        {
            _personManagement.AddPerson(request);
            return new ActionResponse()
            {
                Data = true,
                Message = $"El nuevo contacto {request.Name} fue agregado"
            };
        }

    }
}
