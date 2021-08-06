using System.Collections.Generic;
using Mxss.Schedules.Service.Entities;

namespace Mxss.Schedules.Service.Models.Responses.Person
{
    public class CatPersonDetailResponse: BaseResponse<List<CatPersonDetail>> {}
    public class CatPersonDetail
    {
        public CatPersonDetail(CatPerson catPerson)
        {
            Id = catPerson.Id;
            Name = catPerson.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}