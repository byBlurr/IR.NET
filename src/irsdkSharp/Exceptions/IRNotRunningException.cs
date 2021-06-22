using System;

namespace iRacing.Exceptions
{
    [Serializable]
    public class IRNotRunningException : Exception
    {
        public IRNotRunningException() : base("iRacing is not running on this device.") { }
        public IRNotRunningException(Exception inner) : base("iRacing is not running on this device.", inner) { }

    }
}
