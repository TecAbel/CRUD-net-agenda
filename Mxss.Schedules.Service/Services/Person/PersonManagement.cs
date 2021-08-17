using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using Mxss.Schedules.Enum;
using Mxss.Schedules.Service.Entities;
using Mxss.Schedules.Service.Extensions;
using Mxss.Schedules.Service.Models.Requests;
using Mxss.Schedules.Service.Models.Responses.Person;
using Mxss.Schedules.Service.Models.Settings;

namespace Mxss.Schedules.Service.Services.Person
{
    public interface IPersonManagement
    {
        List<PersonDetail> Retrieve(PersonRequest filters);
        void AddPerson(NewPersonRequest newPersonRequest, Guid? personId);
        PersonDetail GetPersonById(Guid id);

        List<PersonDetail> GetPersonsByName(string name);
    }

    public class PersonManagement : IPersonManagement
    {
        private readonly AppSettings _appSettings;

        public PersonManagement(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public List<PersonDetail> Retrieve(PersonRequest filters)
        {
            MxChavosSql dbContext = new MxChavosSql(_appSettings);

            List<PersonDetail> persons =
                dbContext.People.Select(x => new PersonDetail(x)).ToList();

            BusinessRule.ValidateFalse(persons.Any(), "No se encontraron contactos registrados en el sistema");

            // apply filters
            persons =
                persons.Where(x =>
                    (filters.Name.IsNullOrEmpty() ||
                     x.FullName.Contains(filters.Name, StringComparison.OrdinalIgnoreCase)) &&
                    (filters.Phone.IsNullOrEmpty() || x.Phone.Contains(filters.Phone)) &&
                    (filters.Email.IsNullOrEmpty() ||
                     x.Email.Contains(filters.Email, StringComparison.OrdinalIgnoreCase)) &&
                    (!filters.TypePerson.HasValue || x.PersonType == filters.TypePerson.Value.Description())).ToList();

            BusinessRule.ValidateFalse(persons.Any(),
                "No se encontraron contactos con los criterios de búsqueda ingresados");

            return persons;
        }

        public void AddPerson(NewPersonRequest newPersonRequest, Guid? personId)
        {
            var dbContext = new MxChavosSql(_appSettings);
            var personType = dbContext.CatPeople.Find(newPersonRequest.PersonType);

            BusinessRule.Validate(personType == null,
                $"El tipo de persona con el que intenta registrar a {newPersonRequest.Name} {newPersonRequest.LastName} no es válida");

            if (personId != null)
            {
                var person = dbContext.People.Find(personId);
                BusinessRule.Validate(person == null, "El contacto que intenta actualizar no está registrado en la base de datos");
                if (person != null)
                {
                    person.Updated = DateTime.Now;
                    person.PersonType = newPersonRequest.PersonType;
                    person.Email = newPersonRequest.Email;

                    var concatDb = person.Name + person.LastName + person.Phone;
                    var concatReq = newPersonRequest.Name + newPersonRequest.LastName + newPersonRequest.Phone;
                    BusinessRule.Validate(concatDb == concatReq, "Es necesario que alguno de los campos nombre," +
                                                                 " apellidos y/o número de teléfono sean distintos a los originales " +
                                                                 "para poder actualizar el contacto");
                    person.Name = newPersonRequest.Name;
                    person.LastName = newPersonRequest.LastName;
                    person.Phone = newPersonRequest.Phone;
                    
                    dbContext.People.Update(person);
                }
            }
            else
            {
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
            }

            dbContext.SaveChanges();
        }

        public PersonDetail GetPersonById(Guid id)
        {
            MxChavosSql dbContext = new MxChavosSql(_appSettings);

            var contact = dbContext.People.Find(id);

            BusinessRule.Validate(contact == null, $"El contacto que busca no existe");

            return new PersonDetail(contact);
        }

        public List<PersonDetail> GetPersonsByName(string name)
        {
            name ??= "";
            var dbContext = new MxChavosSql(_appSettings);
            var persons = dbContext.People
                .Where(x => x.Name.ToLower().Contains(name.ToLower()))
                .Select(x => new PersonDetail(x))
                .ToList();
            Console.WriteLine(persons);
            BusinessRule.Validate(persons.Any(), $"No existe un contacto que coincida con {name}");
            return persons;
        }
    }
}