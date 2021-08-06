using Mxss.Schedules.Service.Models;

namespace Mxss.Schedules.Service.Extensions
{
    public static class BusinessRule
    {
        public static void Validate(bool condition, string message)
        {
            if (condition)
            {
                throw new PersonsException(message);
            }
        }
    }
}