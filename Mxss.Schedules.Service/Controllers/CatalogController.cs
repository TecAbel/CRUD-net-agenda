
using Microsoft.AspNetCore.Mvc;
using Mxss.Schedules.Service.Models.Responses.Person;
using Mxss.Schedules.Service.Services.Catalog;

namespace Mxss.Schedules.Service.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : Controller
    {

        private readonly ICatalogManagement _catalogManagement;

        public CatalogController(ICatalogManagement catalogManagement)
        {
            _catalogManagement = catalogManagement;
        }
        // GET
        /// <summary>
        /// Obtiene los tipos de personas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("[Action]")]
        public ActionResult<CatPersonDetailResponse> Retrieve () {
            var result = _catalogManagement.Retrieve();
            return new CatPersonDetailResponse()
            {
                Data = result,
                Message = "Catálogo obtenido correctamente"
            };
        }
    }
}