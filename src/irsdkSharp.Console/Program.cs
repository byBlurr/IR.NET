using iRacing.Exceptions;
using iRacing.Serialization;
using iRacing.Serialization.Models.Session;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iRacing.ConsoleTest
{
    class Program
    {
        private static IRClient Client;

        private static bool _hasConnected;
        private static bool _IsConnected = false;

        private static int waitTime;
        private static IRacingSessionModel _session;

        private static double _TelemetryUpdateFrequency;
        /// <summary>
        /// Gets or sets the number of times the telemetry info is updated per second. The default and maximum is 60 times per second.
        /// </summary>
        public static double TelemetryUpdateFrequency
        {
            get { return _TelemetryUpdateFrequency; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("TelemetryUpdateFrequency must be at least 1.");
                if (value > 60)
                    throw new ArgumentOutOfRangeException("TelemetryUpdateFrequency cannot be more than 60.");

                _TelemetryUpdateFrequency = value;

                waitTime = (int)Math.Floor(1000f / value) - 1;
            }
        }

        /// <summary>
        /// The time in milliseconds between each check if iRacing is running. Use a low value (hundreds of milliseconds) to respond quickly to iRacing startup.
        /// Use a high value (several seconds) to conserve resources if an immediate response to startup is not required.
        /// </summary>
        public static int ConnectSleepTime
        {
            get; set;
        }

        private static int _DriverId;
        /// <summary>
        /// Gets the Id (CarIdx) of yourself (the driver running this application).
        /// </summary>
        public static int DriverId { get { return _DriverId; } }


        static void Main(string[] args)
        {
            Client = IRClient.GetInstance();
            ConnectSleepTime = 1000;
            Task.Run(() => Loop());

            System.Console.ReadLine();
        }

        private static void Loop()
        {
            int lastUpdate = -1;

            while (true)
            {
                // Check if we can find the sim
                if (Client.IsConnected())
                {
                    _hasConnected = true;
                    _IsConnected = true;

                    int attempts = 0;
                    const int maxAttempts = 99;

                    object sessionnum = TryGetSessionNum();
                    while (sessionnum == null && attempts <= maxAttempts)
                    {
                        attempts++;
                        sessionnum = TryGetSessionNum();
                    }
                    if (attempts >= maxAttempts)
                    {
                        System.Console.WriteLine("Session num too many attempts");
                        continue;
                    }

                    // Parse out your own driver Id
                    if (DriverId == -1)
                    {
                        _DriverId = (int)Client.GetData("PlayerCarIdx");
                    }

                    var data = Client.GetSerializedData();

                    // Is the session info updated?
                    int newUpdate = Client.Header.SessionInfoUpdate;
                    if (newUpdate != lastUpdate)
                    {
                        lastUpdate = newUpdate;
                        _session = Client.GetSerializedSessionInfo();
                    }

                    if(data != null && _session != null)
                    {
                        Console.SetCursorPosition(0,0);


                        foreach (var car in data.Data.Cars.OrderByDescending(x => x.CarIdxLap).ThenByDescending(x => x.CarIdxLapDistPct))
                        {
                            var currentData = _session.DriverInfo.Drivers.Where(y => y.CarIdx == car.CarIdx).FirstOrDefault();
                            if (currentData != null && car.CarIdxEstTime != 0)
                            {
                                Console.WriteLine($"{currentData.CarNumber}\t{string.Format("{0:0.00}", car.CarIdxEstTime)}\t{string.Format("{0:0.00}", car.CarIdxLapDistPct * 100)}");
                            }

                        }
                    }

                }
                else if (_hasConnected)
                {
                    Client.Shutdown();
                    _DriverId = -1;
                    lastUpdate = -1;
                    _IsConnected = false;
                    _hasConnected = false;
                }
                else
                {
                    _IsConnected = false;
                    _hasConnected = false;
                    _DriverId = -1;

                    //Try to find the sim
                    try
                    {
                        Client.Startup();
                    }
                    catch (IRNotRunningException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                // Sleep for a short amount of time until the next update is available
                if (_IsConnected)
                {
                    if (waitTime <= 0 || waitTime > 1000) waitTime = 250;
                    Thread.Sleep(waitTime);
                }
                else
                {
                    // Not connected yet, no need to check every 16 ms, let's try again in some time
                    Thread.Sleep(ConnectSleepTime);
                }
            }

        }
        private static object TryGetSessionNum()
        {
            try
            {
                var sessionnum = Client.GetData("SessionNum");
                return sessionnum;
            }
            catch
            {
                return null;
            }
        }
    }
}
