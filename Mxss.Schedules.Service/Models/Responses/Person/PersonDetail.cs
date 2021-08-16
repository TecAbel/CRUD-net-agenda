using System;
using System.Collections.Generic;
using Mxss.Schedules.Enum;

namespace Mxss.Schedules.Service.Models.Responses.Person
{
    public class PersonDetailListResponse : BaseResponse<List<PersonDetail>> {}
    public class PersonDetail
    {
        public PersonDetail(Entities.Person person)
        {
            Id = person.PersonId;
            PersonType = person.PersonType.ToEnum<TypePerson>().Description();
            FullName = person.FullName();
            Phone = person.Phone;
            Email = person.Email;
            RegisterDate = person.Registered;
            UpdatedDate = person.Updated;
        }

        public Guid Id { get; set; }
        public string PersonType { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}