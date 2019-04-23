using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Java.Security;
using Xamarin.Essentials;

namespace XamarinEssentials
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private Essentials.EConnectivity Connectivity = new Essentials.EConnectivity();
        private Essentials.EBattery Battery = new Essentials.EBattery();
        private Essentials.EFlashlight Flashlight = new Essentials.EFlashlight();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Platform.Init(this, savedInstanceState); // Needed for the flashlight to work

            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            // Set battery value to textview
            Battery.GetBattery();
            var bLevel = Battery.Level;
            var bTextView = FindViewById<TextView>(Resource.Id.textViewBattery);
            bTextView.Text = $"Battery: {bLevel * 100}%";

            // Set app info values to textview
            Essentials.EAppInfo appInfo = new Essentials.EAppInfo();
            var aiTextView = FindViewById<TextView>(Resource.Id.textViewAppInfo);
            aiTextView.Text = $"AppName: {appInfo.AppName} \n" +
                              $"Build: {appInfo.Build} \n" +
                              $"PackageName: {appInfo.PackageName} \n" +
                              $"Version: {appInfo.Version}";

            // Checks internet connection on button click and sets the boolean value to textview
            var cTextView = FindViewById<TextView>(Resource.Id.textViewConnectivity);
            var cButton = FindViewById<Button>(Resource.Id.btnConnectivity);
            cButton.Click += (sender, e) =>
                cTextView.Text = $"IsConnection: {Connectivity.CheckConnectivity().ToString()}";

            // Sets display info variables to textview
            Essentials.EDisplayInfo eDisplayInfo = new Essentials.EDisplayInfo();
            var diTextView = FindViewById<TextView>(Resource.Id.textViewDisplayInfo);
            diTextView.Text = $"Width: {eDisplayInfo.Width} \n" +
                              $"Height: {eDisplayInfo.Height} \n" +
                              $"Orientation: {eDisplayInfo.Orientation} \n" +
                              $"Rotation:{eDisplayInfo.Rotation}\n" +
                              $"Density: {eDisplayInfo.Density}";

            // Flashlight on/off
            var toggleButton = FindViewById<ToggleButton>(Resource.Id.btnFlashlightToggle);

            toggleButton.Click += (sender, e) =>
            {
                if (toggleButton.Checked)
                    Flashlight.FlashLightOn();
                else
                    Flashlight.FlashLightOff();
            };
        }
    }
}