using System;
using System.Collections.Generic;
using System.Text;

namespace iRacing.Serialization.Models.Session.RadioInfo
{
    public class RadioInfoModel
    {
        public int SelectedRadioNum { get; set; }
        public List<RadioModel> Radios { get; set; }
    }
}
