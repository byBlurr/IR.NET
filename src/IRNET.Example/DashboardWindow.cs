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
        private IRacingSessionModel CurrentSession = null;

        private float MaxRevs = 7400;
        private float OptimumShiftMin = 6400;
        private float OptimumShiftMax = 6800;
        private float GoodShiftMin = 6000;
        private float MinRpm = 5200;

        public DashboardWindow()
        {
            InitializeComponent();

            Client = new IRClient();
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
                        }
                    }
                    else
                    {
                        Debug.WriteLine("Couldn't find session...");
                    }
                }
                else
                {
                    DriverId = -1;

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
                GearIndicator.Text = "-";
                RevsIndicator.Text = "---rpm";
                SpeedIndicator.Text = "---mph";
                BestTime.Text = "--:--.---";
                CurrentLap.Text = "--:--.---";
            }
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
