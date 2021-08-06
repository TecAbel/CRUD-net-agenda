using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Mxss.Schedules.Service.Entities;
using Mxss.Schedules.Service.Extensions;
using Mxss.Schedules.Service.Models.Requests;
using Mxss.Schedules.Service.Models.Responses.Person;
using Mxss.Schedules.Service.Models.Settings;

namespace Mxss.Schedules.Service.Services.Person
{
    public interface IPersonManagement
    {
        List<PersonDetail> Retrieve();
        void AddPerson(NewPersonRequest newPersonRequest);
    }

    public class PersonManagement: IPersonManagement
    {
        private readonly AppSettings _appSettings;
        public PersonManagement(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public List<PersonDetail> Retrieve()
        {
            MxChavosSql dbContext = new MxChavosSql(_appSettings);

            return
                dbContext
                    .People
                    .Select(x => new PersonDetail(x))
                    .ToList();

        }

        public void AddPerson(NewPersonRequest newPersonRequest)
        {
            Console.WriteLine(newPersonRequest.ToString());
            var dbContext = new MxChavosSql(_appSettings);
            var personType = dbContext.CatPeople.Find(newPersonRequest.PersonType);

            int x = Convert.ToInt32( newPersonRequest.Name);
            
            BusinessRule.Validate(
            personType == null,
             $"El tipo de persona con el que intenta registrar a {newPersonRequest.Name} {newPersonRequest.LastName} no es válida"
             );

            var personToAdd = new Entities.Person()
            {
                PersonId = Guid.NewGuid(),
                Name = newPersonRequest.Name,
                LastName = newPersonRequest.LastName,
                PersonType = newPersonRequest.PersonType,
                Email = newPersonRequest.Email,
                Phone = newPersonRequest.Phone,
                Registered = DateTime.Now
            };

            dbContext.People.Add(personToAdd);

            dbContext.SaveChanges();
        }
    }
}
