using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

namespace Demo.Features.Android
{
    [Activity(Label = "Demo.Features.Android"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Landscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var game = new AndroidGameMain(new PlatformConfig { IsFullScreen = true });
            var view = (View)game.Services.GetService(typeof(View));

            HideSystemUi();

            SetContentView(view);
            game.Run();
        }

        private void HideSystemUi()
        {
            // https://developer.android.com/training/system-ui/immersive.html

            if (Build.VERSION.SdkInt >= (BuildVersionCodes)19)
            {
                var decorView = Window.DecorView;
                var uiVisibility = (int)decorView.SystemUiVisibility;
                var options = uiVisibility;

                options |= (int)SystemUiFlags.LowProfile;
                options |= (int)SystemUiFlags.Fullscreen;
                options |= (int)SystemUiFlags.HideNavigation;
                options |= (int)SystemUiFlags.ImmersiveSticky;

                decorView.SystemUiVisibility = (StatusBarVisibility)options;
            }
        }
    }
}

