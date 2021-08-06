using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Mxss.Schedules.Service.Entities;
using Mxss.Schedules.Service.Extensions;
using Mxss.Schedules.Service.Models.Responses.Person;
using Mxss.Schedules.Service.Models.Settings;

namespace Mxss.Schedules.Service.Services.Catalog
{
    public interface ICatalogManagement
    {
        List<CatPersonDetail> Retrieve();
        CatPersonDetail ById(int id);
    }

    public class CatalogManagement : ICatalogManagement
    {
        private readonly AppSettings _settings;

        public CatalogManagement(IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
        }

        public List<CatPersonDetail> Retrieve()
        {
            MxChavosSql mxChavosSql = new MxChavosSql(_settings);

            return mxChavosSql
                .CatPeople
                .Select(x => new CatPersonDetail(x))
                .ToList();
        }

        public CatPersonDetail ById(int id)
        {
            var dbContext = new MxChavosSql(_settings);

            var person = dbContext.CatPeople.Find(id);
            
            BusinessRule.Validate(person == null, $"No se pudo encontrar el tipo de persona con Id {id}");

            return new CatPersonDetail(person);
        }
    }
}