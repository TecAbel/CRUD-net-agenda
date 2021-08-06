using System;
using System.Collections.Generic;
using Mxss.Schedules.Service.Models.Requests;

#nullable disable

namespace Mxss.Schedules.Service.Entities
{
    public partial class Person
    {
        public Guid PersonId { get; set; }
        public int PersonType { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Registered { get; set; }
        public DateTime? Updated { get; set; }

        public virtual CatPerson PersonTypeNavigation { get; set; }
    }
}
