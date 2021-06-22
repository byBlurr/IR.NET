using iRacing.Serialization.Models.Session.CameraInfo;
using iRacing.Serialization.Models.Session.DriverInfo;
using iRacing.Serialization.Models.Session.QualifyResultsInfo;
using iRacing.Serialization.Models.Session.RadioInfo;
using iRacing.Serialization.Models.Session.SessionInfo;
using iRacing.Serialization.Models.Session.SplitTimeInfo;
using iRacing.Serialization.Models.Session.WeekendInfo;
using System;
using System.IO;
using YamlDotNet.Serialization;

namespace iRacing.Serialization.Models.Session
{
    public class IRSessionModel
    {
        public static IRSessionModel Serialize(string yaml)
        {
            if (yaml.IndexOf("CarSetup:") != -1)
            {
                yaml = yaml.Substring(0, yaml.IndexOf("CarSetup:")) + "...";
            }

            var r = new StringReader(yaml);
            var deserializer = new DeserializerBuilder().Build();
            try
            {
                return deserializer.Deserialize<IRSessionModel>(r);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return null;
            }
        }

        public WeekendInfoModel WeekendInfo { get; set; }
        public SessionInfoModel SessionInfo { get; set; }
        public QualifyResultsInfoModel QualifyResultsInfo { get; set; }
        public CameraInfoModel CameraInfo { get; set; }
        public RadioInfoModel RadioInfo { get; set; }
        public DriverInfoModel DriverInfo { get; set; }
        public SplitTimeInfoModel SplitTimeInfo { get; set; }

    }
}
