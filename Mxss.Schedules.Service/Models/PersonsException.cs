using System;

namespace Mxss.Schedules.Service
{
    [Serializable]
    public class PersonsException : Exception
    {
        public PersonsException() {}
        public PersonsException(string message) : base(message) {}
        public PersonsException(string message, Exception inner) : base(message, inner) {}
        
        protected PersonsException(
                System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context
        ): base(info, context) {}
    }
}