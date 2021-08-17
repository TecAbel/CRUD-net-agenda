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

        public static void ValidateFalse(bool condition, string messageCode)
        {
            Validate(!condition, messageCode);
        }
    }
}