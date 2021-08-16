using System;
using System.Collections.Generic;
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
        public ActionResult<PersonDetailListResponse> Retrieve([FromQuery] PersonRequest filters)
        {
            var res = _personManagement.Retrieve(filters);
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

        /// <summary>
        /// Obtiene contacto por id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("contact/{id}")]
        [CatchException("Ha ocurrido un error al intentar obtener el contacto")]
        public ActionResult<BaseResponse<PersonDetail>> PersonById(Guid id)
        {
            Console.WriteLine(id);
            var person = _personManagement.GetPersonById(id);
            Console.WriteLine(person);
            return new BaseResponse<PersonDetail>()
            {
                Data = person,
                Message = "Se obtuvo el contacto correctamente"
            };
        }

        
        /// <summary>
        /// Obtiene los contactos por nombre
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("contact")]
        [CatchException("Ha ocurrido un error al intentar obtener los contactos")]
        public ActionResult<BaseResponse<List<PersonDetail>>> GetByName([FromQuery] string name)
        {
            var persons = _personManagement.GetPersonsByName(name);
            return new BaseResponse<List<PersonDetail>>()
            {
                Data = persons,
                Message = "Se obtuvieron los contactos correctamente"
            };
        }

    }
}
