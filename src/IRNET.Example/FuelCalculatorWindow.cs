using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRNET.Example
{
    public partial class FuelCalculatorWindow : Form
    {
        private bool ShowLapsLeft;
        private bool ShowFuelBar;
        private bool ShowAvgLapFuel;
        private bool ShowFastLapFuel;
        private bool ShowRefuelAmount;

        public FuelCalculatorWindow()
        {
            InitializeComponent();
        }

        public void UpdateSettings(Settings settings)
        {
            ShowLapsLeft = settings.ShowLapsLeft;
            ShowFuelBar = settings.ShowFuelBar;
            ShowAvgLapFuel = settings.ShowAvgLapFuel;
            ShowFastLapFuel = settings.ShowFastLapFuel;
            ShowRefuelAmount = settings.ShowRefuelAmount;
        }
    }
}
