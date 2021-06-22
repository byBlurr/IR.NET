using System.Collections.Generic;

namespace iRacing.Serialization.Models.Session.RadioInfo
{
    public class RadioInfoModel
    {
        public int SelectedRadioNum { get; set; }
        public List<RadioModel> Radios { get; set; }
    }
}
