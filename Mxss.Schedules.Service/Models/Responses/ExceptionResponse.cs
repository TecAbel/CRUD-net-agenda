using System;

namespace Mxss.Schedules.Service.Models.Responses
{
    public class ExceptionResponse: BaseResponse<bool>
    {
        public Exception Exception { get; set; }
    }
}