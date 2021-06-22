﻿using System;
using System.Collections.Generic;
using System.Text;

namespace irsdkSharp.Exceptions
{
    [Serializable]
    public class IRNotFoundException : Exception
    {
        public IRNotFoundException() : base("No data found for iRacing. Is iRacing open?") { }
        public IRNotFoundException(Exception inner) : base("No data found for iRacing. Is iRacing open?", inner) { }

    }
}
