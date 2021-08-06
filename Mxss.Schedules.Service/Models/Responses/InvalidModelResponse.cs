using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Mxss.Schedules.Service.Models.Responses
{
    public class InvalidModelResponse: BaseResponse<List<string>>
    {

        public List<string> ErrorList { get; set; }

        public InvalidModelResponse(ModelStateDictionary modelStateDictionary)
        {
            ErrorList = new List<string>();

            foreach (var item in modelStateDictionary.Values)
            {
                foreach (var error in item.Errors)
                {
                    ErrorList.Add(error.ErrorMessage);
                }
            }
        }
    }
}