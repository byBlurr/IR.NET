using System.Collections.Generic;

namespace iRacing.Serialization.Models.Session.SessionInfo
{
    public class SessionInfoModel
    {
        public int NumSessions { get; set; }
        public List<SessionModel> Sessions { get; set; }
    }
}
