using System;
using Android.Hardware.Camera2;
using Xamarin.Essentials;
using FlagsAttribute = System.FlagsAttribute;

namespace XamarinEssentials
{
    public class Essentials
    {
        #region Battery
        public class EBattery
        {
        public EBattery()
            {
                // Register for battery changes, be sure to unsubscribe when needed
                Battery.BatteryInfoChanged += Battery_BatteryInfoChanged;
            }

        public double Level;

            public void GetBattery()
            {
                Level = Battery.ChargeLevel; // returns 0.0 to 1.0 or 1.0 when on AC or no battery.

                var state = Battery.State;

                switch (state)
                {
                    case BatteryState.Charging:
                        // Currently charging
                        break;
                    case BatteryState.Full:
                        // Battery is full
                        break;
                    case BatteryState.Discharging:
                    case BatteryState.NotCharging:
                        // Currently discharging battery or not being charged
                        break;
                    case BatteryState.NotPresent:
                    // Battery doesn't exist in device (desktop computer)
                    case BatteryState.Unknown:
                        // Unable to detect battery state
                        break;
                }

                var source = Battery.PowerSource;

                switch (source)
                {
                    case BatteryPowerSource.Battery:
                        // Being powered by the battery
                        break;
                    case BatteryPowerSource.AC:
                        // Being powered by A/C unit
                        break;
                    case BatteryPowerSource.Usb:
                        // Being powered by USB cable
                        break;
                    case BatteryPowerSource.Wireless:
                        // Powered via wireless charging
                        break;
                    case BatteryPowerSource.Unknown:
                        // Unable to detect power source
                        break;
                }
            }

            void Battery_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
            {
                var level = e.ChargeLevel;
                var state = e.State;
                var source = e.PowerSource;
                Console.WriteLine($"Reading: Level: {level}, State: {state}, Source: {source}");
            }
        }
        #endregion

        #region AppInfo
        /// <summary>
        /// Call GetAppInfo() before accessing variables.
        /// </summary>
        public class EAppInfo
        {
            public string AppName;
            public string PackageName;
            public string Version;
            public string Build;

            // Constructor
            public EAppInfo()
            {
                // Application Name
                AppName = AppInfo.Name;

                // Package Name/Application Identifier (com.microsoft.testapp)
                PackageName = AppInfo.PackageName;

                // Application Version (1.0.0)
                Version = AppInfo.VersionString;

                // Application Build Number (1)
                Build = AppInfo.BuildString;
            }
        }
        #endregion

        #region Connectivity
        /// <summary>
        /// Returns true if there is an internet connection.
        /// </summary>
        public class EConnectivity
        {
            public bool CheckConnectivity()
            {
                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    // Connection to internet is available
                    Console.WriteLine("Connection to internet is available.");
                    return true;
                }

                if (current == NetworkAccess.None)
                {
                    // Connection to internet is not available
                    Console.WriteLine("Connection to internet is not available.");
                    return false;
                }

                // Default
                return false;
            }
        }
        #endregion

        #region Display Information
        public class EDisplayInfo
        {
            public DisplayOrientation Orientation;
            public DisplayRotation Rotation;
            public double Width;
            public double Height;
            public double Density;

            public EDisplayInfo()
            {
                // Get Metrics
                var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

                // Orientation (Landscape, Portrait, Square, Unknown)
                Orientation = mainDisplayInfo.Orientation;

                // Rotation (0, 90, 180, 270)
                Rotation = mainDisplayInfo.Rotation;

                // Width (in pixels)
                Width = mainDisplayInfo.Width;

                // Height (in pixels)
                Height = mainDisplayInfo.Height;

                // Screen density
                Density = mainDisplayInfo.Density;
            }
        }
        #endregion

        #region Flashlight
        public class EFlashlight
        {
            public async void FlashLightOn()
            {
                try
                {
                    // Turn On
                    await Flashlight.TurnOnAsync();
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to turn on/off flashlight
                }
            }

            public async void FlashLightOff()
            {
                try
                {
                    // Turn Off
                    await Flashlight.TurnOffAsync();
                }
                catch (FeatureNotSupportedException fnsEx)
                {
                    // Handle not supported on device exception
                }
                catch (PermissionException pEx)
                {
                    // Handle permission exception
                }
                catch (Exception ex)
                {
                    // Unable to turn on/off flashlight
                }
            }
        }
        #endregion
    }
}