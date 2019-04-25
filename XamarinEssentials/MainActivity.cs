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
            bTextView.Text = $"{Resources.GetText(Resource.String.Battery)}: {bLevel * 100}";

            // Set app info values to textview
            Essentials.EAppInfo appInfo = new Essentials.EAppInfo();
            var aiTextView = FindViewById<TextView>(Resource.Id.textViewAppInfo);
            aiTextView.Text = $"{Resources.GetText(Resource.String.AppName)}: {appInfo.AppName} \n" +
                              $"{Resources.GetText(Resource.String.Build)}: {appInfo.Build} \n" +
                              $"{Resources.GetText(Resource.String.PackageName)}: {appInfo.PackageName} \n" +
                              $"{Resources.GetText(Resource.String.Version)}: {appInfo.Version}";

            // Checks internet connection on button click and sets the boolean value to textview
            var cTextView = FindViewById<TextView>(Resource.Id.textViewConnectivity);
            var cButton = FindViewById<Button>(Resource.Id.btnConnectivity);
            cButton.Click += (sender, e) =>
                cTextView.Text = $"{Resources.GetText(Resource.String.Connection)}: {Connectivity.CheckConnectivity().ToString()}";

            // Sets display info variables to textview
            Essentials.EDisplayInfo eDisplayInfo = new Essentials.EDisplayInfo();
            var diTextView = FindViewById<TextView>(Resource.Id.textViewDisplayInfo);
            diTextView.Text = $"{Resources.GetText(Resource.String.Width)}: {eDisplayInfo.Width} \n" +
                              $"{Resources.GetText(Resource.String.Height)}: { eDisplayInfo.Height} \n" +
                              $"{Resources.GetText(Resource.String.Orientation)}: {eDisplayInfo.Orientation} \n" +
                              $"{Resources.GetText(Resource.String.Rotation)}:{eDisplayInfo.Rotation}\n" +
                              $"{Resources.GetText(Resource.String.Density)}: {eDisplayInfo.Density}";

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