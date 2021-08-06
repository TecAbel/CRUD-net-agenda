namespace Mxss.Schedules.Service.Models.Responses
{
    public class BaseResponse<T>
    {
        public BaseResponse() {}

        public string Message { get; set; }
        public T Data { get; set; }
    }
}