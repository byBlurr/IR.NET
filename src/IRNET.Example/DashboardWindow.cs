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
        private float OptimumShiftMin = 6400;
        private float OptimumShiftMax = 6800;
        private float GoodShiftMin = 6000;
        private float MinRpm = 5200;

        public DashboardWindow()
        {
            InitializeComponent();

            Client = IRClient.GetInstance();
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
            RevsIndicator.Text = "000rpm";
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

            if (data.RPM > OptimumShiftMax) RevsFore.BackColor = Color.Red;
            else if (data.RPM > OptimumShiftMin) RevsFore.BackColor = Color.Purple;
            else if (data.RPM > GoodShiftMin) RevsFore.BackColor = Color.Yellow;
            else if (data.RPM > MinRpm) RevsFore.BackColor = Color.Green;
            else RevsFore.BackColor = Color.White;

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
