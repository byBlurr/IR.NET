﻿namespace iRacing.Serialization.Models.Session.SessionInfo
{
    public class FastestLapModel
    {
        public int CarIdx { get; set; }// %d
        public int FastestLap { get; set; }// %d
        public float FastestTime { get; set; }// %.3f
    }
}
