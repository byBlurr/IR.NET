using iRacing;
using iRacing.Exceptions;
using iRacing.Serialization;
using iRacing.Serialization.Models.Data;
using iRacing.Serialization.Models.Session;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace IRNET.Example
{
    public partial class DashboardWindow : Form
    {
        private IRClient Client;

        private int DriverId = -1;
        private int LastUpdate = -1;
        private IRSessionModel CurrentSession = null;

        private float MaxRevs = 7400;
        private float OptimumShift = 6400;
        private float Redline = 6800;

        public DashboardWindow()
        {
            InitializeComponent();

            Client = IRClient.GetInstance();
        }

        public void UpdateSettings(Settings settings)
        {
            MaxRevs = settings.MaximumRpm;
            OptimumShift = settings.OptimumShift;
            Redline = settings.Redline;
        }

        private void Update(object sender, EventArgs e)
        {
            if (IRClient.GetSimStatus())
            {
                if (Client.IsConnected())
                {
                    object sessionnum = Client.GetData("SessionNum");
                    if (sessionnum != null)
                    {
                        if (DriverId == -1) DriverId = (int)Client.GetData("PlayerCarIdx");

                        var data = Client.GetSerializedData();
                        int newUpdate = Client.Header.SessionInfoUpdate;
                        if (newUpdate != LastUpdate)
                        {
                            LastUpdate = newUpdate;
                            CurrentSession = Client.GetSerializedSessionInfo();
                        }

                        if (data != null && CurrentSession != null)
                        {
                            Visible = !data.Data.IsInGarage;
                            UpdateDashboard(data.Data);
                        }
                        else
                        {
                            Debug.WriteLine("data or current session is null...");
                            ResetDashboard();
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Couldn't find session...");
                        ResetDashboard();
                    }
                }
                else
                {
                    DriverId = -1;
                    ResetDashboard();

                    try
                    {
                        Client.Startup();
                    }
                    catch (IRNotRunningException)
                    {
                        Debug.WriteLine("Waiting for iRacing to open...");
                    }
                }
            }
            else
            {
                ResetDashboard();
            }
        }

        private void ResetDashboard()
        {
            GearIndicator.ForeColor = Color.Gray;
            GearIndicator.Text = "N";
            RevsFore.BackColor = Color.Gray;
            RevsFore.Width = RevsBack.Width;
            RevsIndicator.ForeColor = Color.Gray;
            RevsIndicator.Text = "0000rpm";
            SpeedIndicator.ForeColor = Color.Gray;
            SpeedIndicator.Text = "000mph";
            BestTime.ForeColor = Color.Gray;
            BestTime.Text = "00:00.000";
            CurrentLap.ForeColor = Color.Gray;
            CurrentLap.Text = "00:00.000";
        }

        private void UpdateDashboard(DataModel data)
        {
            GearIndicator.Text = data.Gear == 0 ? "N" : data.Gear == -1 ? "R" : data.Gear.ToString();
            RevsFore.Width = (int)((float)RevsBack.Width * (data.RPM / MaxRevs));
            RevsIndicator.Text = (int)data.RPM + "rpm";
            SpeedIndicator.Text = (int)data.Speed + "mph";

            int mins = (int)data.LapBestLapTime / 60;
            BestTime.Text = mins + ":" + string.Format("{0:00.000}", (data.LapBestLapTime - (float)(mins * 60)));

            int currentMins = (int)data.LapCurrentLapTime / 60;
            CurrentLap.Text = currentMins + ":" + string.Format("{0:00.000}", (data.LapCurrentLapTime - (float)(currentMins * 60)));

            if (data.LapDeltaToSessionOptimalLap_OK) CurrentLap.ForeColor = Color.Purple;
            else if (data.LapDeltaToSessionBestLap_OK) CurrentLap.ForeColor = Color.Green;
            else CurrentLap.ForeColor = Color.Red;

            if (data.RPM > Redline) RevsFore.BackColor = Color.Red;
            else if (data.RPM > OptimumShift) RevsFore.BackColor = Color.Purple;
            else RevsFore.BackColor = Color.Green;

            GearIndicator.ForeColor = Color.DarkGray;
            RevsIndicator.ForeColor = Color.DarkGray;
            SpeedIndicator.ForeColor = Color.DarkGray;
            BestTime.ForeColor = Color.DarkGray;
        }

        private void SettingsMenuItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text.Equals("Close"))
            {
                this.Close();
            }
        }
    }
}
