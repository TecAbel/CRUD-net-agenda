using System;
using System.Collections.Generic;

#nullable disable

namespace Mxss.Schedules.Service.Entities
{
    public partial class CatPerson
    {
        public CatPerson()
        {
            People = new HashSet<Person>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
