using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows.Forms;

namespace IRNET.Example
{
    public partial class MainWindow : Form
    {
        private Settings settings;
        private DashboardWindow Dashboard = null;
        private FuelCalculatorWindow FuelCalculator = null;

        public MainWindow()
        {
            InitializeComponent();

            settings = Settings.Load();
            DashboardToggle.Checked = settings.EnableDashboard;
            FuelCalculatorToggle.Checked = settings.EnableFuelCalculator;
            UpdateSettings();

            if (DashboardToggle.Checked)
            {
                Dashboard = new DashboardWindow();
                Dashboard.UpdateSettings(settings);
                Dashboard.Show();
            }
            if (FuelCalculatorToggle.Checked)
            {
                FuelCalculator = new FuelCalculatorWindow();
                FuelCalculator.UpdateSettings(settings);
                FuelCalculator.Show();
            }
        }

        private void UpdateSettings()
        {
            // Dashboard
            MaxRevsBox.Text = settings.MaximumRpm.ToString();
            OptimumRevsBox.Text = settings.OptimumShift.ToString();
            RedlineRevsBox.Text = settings.Redline.ToString();

            UpdateDashboardButton.Enabled = DashboardToggle.Checked;
            MaxRevsBox.Enabled = DashboardToggle.Checked;
            MaxRPMLabel.Enabled = DashboardToggle.Checked;
            OptimumRevsBox.Enabled = DashboardToggle.Checked;
            OptShiftLabel.Enabled = DashboardToggle.Checked;
            RedlineRevsBox.Enabled = DashboardToggle.Checked;
            RedlineLabel.Enabled = DashboardToggle.Checked;

            // Fuel Calculator
            LapsLeftToggle.Checked = settings.ShowLapsLeft;
            LapsLeftToggle.Enabled = FuelCalculatorToggle.Checked;
            FuelBarToggle.Checked = settings.ShowFuelBar;
            FuelBarToggle.Enabled = FuelCalculatorToggle.Checked;
            AvgLapFuelToggle.Checked = settings.ShowAvgLapFuel;
            AvgLapFuelToggle.Enabled = FuelCalculatorToggle.Checked;
            FastLapFuelToggle.Checked = settings.ShowFastLapFuel;
            FastLapFuelToggle.Enabled = FuelCalculatorToggle.Checked;
            RefuelToggle.Checked = settings.ShowRefuelAmount;
            RefuelToggle.Enabled = FuelCalculatorToggle.Checked;
        }

        private void ToggleOverlays(object sender, EventArgs e)
        {
            UpdateSettings();

            // Dashboard
            if (DashboardToggle.Checked)
            {
                if (Dashboard == null)
                {
                    settings.EnableDashboard = true;
                    Dashboard = new DashboardWindow();
                    Dashboard.Show();
                }
            }
            else
            {
                if (Dashboard != null)
                {
                    settings.EnableDashboard = false;
                    Dashboard.Close();
                    Dashboard.Dispose();
                    Dashboard = null;
                }
            }

            // Fuel Calculator
            if (FuelCalculatorToggle.Checked)
            {
                settings.EnableFuelCalculator = true;
                FuelCalculator = new FuelCalculatorWindow();
                FuelCalculator.Show();
            }
            else
            {
                if (FuelCalculator != null)
                {
                    settings.EnableFuelCalculator = false;
                    FuelCalculator.Close();
                    FuelCalculator.Dispose();
                    FuelCalculator = null;
                }
            }

            settings.Save();
        }

        private void RevsChanged(object sender, EventArgs e)
        {
           int newMaxRpm, newOptShift, newRedline;

            try
            {
                newMaxRpm = int.Parse(MaxRevsBox.Text);
            }
            catch
            {
                newMaxRpm = settings.MaximumRpm;
            }

            try
            {
                newOptShift = int.Parse(OptimumRevsBox.Text);
            }
            catch
            {
                newOptShift = settings.OptimumShift;
            }

            try
            {
                newRedline = int.Parse(RedlineRevsBox.Text);
            }
            catch
            {
                newRedline = settings.Redline;
            }

            if (newMaxRpm > newOptShift && newMaxRpm > newRedline && newRedline > newOptShift)
            {
                settings.MaximumRpm = newMaxRpm;
                settings.OptimumShift = newOptShift;
                settings.Redline = newRedline;
                settings.Save();
            }

            UpdateSettings();
            if (Dashboard != null) Dashboard.UpdateSettings(settings);
            if (FuelCalculator != null) FuelCalculator.UpdateSettings(settings);
        }

        private void FuelCalcChanged(object sender, EventArgs e)
        {
            settings.ShowLapsLeft = LapsLeftToggle.Checked;
            settings.ShowFuelBar = FuelBarToggle.Checked;
            settings.ShowAvgLapFuel = AvgLapFuelToggle.Checked;
            settings.ShowFastLapFuel = FastLapFuelToggle.Checked;
            settings.ShowRefuelAmount = RefuelToggle.Checked;

            settings.Save();
        }
    }

    public class Settings
    {
        // Dashboard Settings
        public bool EnableDashboard { get; set; }
        public int MaximumRpm { get; set; }
        public int OptimumShift { get; set; }
        public int Redline { get; set; }

        public bool EnableFuelCalculator { get; set; }
        public bool ShowFuelBar { get; set; }
        public bool ShowAvgLapFuel { get; set; }
        public bool ShowFastLapFuel { get; set; }
        public bool ShowLapsLeft { get; set; }
        public bool ShowRefuelAmount { get; set; }

        [JsonIgnore]
        private static string SettingsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "iRacing", "blurr", "settings.json");

        public Settings()
        {
            EnableDashboard = false;
            MaximumRpm = 7000;
            OptimumShift = 5000;
            Redline = 6000;

            EnableFuelCalculator = false;
            ShowFuelBar = true;
            ShowAvgLapFuel = true;
            ShowFastLapFuel = true;
            ShowLapsLeft = true;
            ShowRefuelAmount = true;
        }

        public static Settings Load()
        {
            if (!File.Exists(SettingsPath))
            {
                Settings settings = new Settings();
                settings.Save();
            }

            return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(SettingsPath));
        }

        public void Save()
        {
            if (!Directory.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "iRacing", "blurr"))) Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "iRacing", "blurr"));
            File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(this));
        }
    }
}
