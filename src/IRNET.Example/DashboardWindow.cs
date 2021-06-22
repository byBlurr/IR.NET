using iRacing;
using iRacing.Exceptions;
using iRacing.Serialization;
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
                            GearIndicator.Text = data.Data.Gear == 0 ? "N" : data.Data.Gear.ToString();
                            RevsFore.Width = (int)((float)RevsBack.Width * (data.Data.RPM / MaxRevs));
                            RevsIndicator.Text = (int)data.Data.RPM + "rpm";
                            SpeedIndicator.Text = (int)data.Data.Speed + "mph";

                            int mins = (int)data.Data.LapBestLapTime / 60;
                            BestTime.Text = mins + ":" + string.Format("{0:00.000}", (data.Data.LapBestLapTime - (float)(mins * 60)));

                            int currentMins = (int)data.Data.LapCurrentLapTime / 60;
                            CurrentLap.Text = currentMins + ":" + string.Format("{0:00.000}", (data.Data.LapCurrentLapTime - (float)(currentMins * 60)));

                            if (data.Data.LapDeltaToSessionOptimalLap_OK) CurrentLap.ForeColor = Color.Purple;
                            else if (data.Data.LapDeltaToSessionBestLap_OK) CurrentLap.ForeColor = Color.Green;
                            else CurrentLap.ForeColor = Color.Red;

                            if (data.Data.RPM > OptimumShiftMax) RevsFore.BackColor = Color.Red;
                            else if (data.Data.RPM > OptimumShiftMin) RevsFore.BackColor = Color.Purple;
                            else if (data.Data.RPM > GoodShiftMin) RevsFore.BackColor = Color.Yellow;
                            else if (data.Data.RPM > MinRpm) RevsFore.BackColor = Color.Green;
                            else RevsFore.BackColor = Color.White;
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
    }
}
