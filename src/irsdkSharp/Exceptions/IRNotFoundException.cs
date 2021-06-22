using System;

namespace iRacing.Exceptions
{
    [Serializable]
    public class IRNotFoundException : Exception
    {
        public IRNotFoundException() : base("No data found for iRacing. Is iRacing open?") { }
        public IRNotFoundException(Exception inner) : base("No data found for iRacing. Is iRacing open?", inner) { }

    }
}
