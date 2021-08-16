using Mxss.Schedules.Service.Entities;

namespace Mxss.Schedules.Service
{
    public static class PersonExtensions
    {
        public static string FullName(this Person value)
        {
            return $"{value.Name} {value.LastName}".Trim();
        }
    }
}