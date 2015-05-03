using System;
using Xamarin.Forms.Platform.Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Content;

namespace RssReaderForms.Android
{
	[Activity (MainLauncher = true,NoHistory=true, Theme="@style/SplashScreen",ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class SplashActivity:Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			this.RequestWindowFeature (global::Android.Views.WindowFeatures.NoTitle);
			base.OnCreate (bundle);
		SetContentView (Resource.Layout.Splash);
		//Xamarin.Forms.Forms.Init (this, bundle);

			Handler handler = new Handler ();
			handler.PostDelayed (new Java.Lang.Runnable (() =>
				StartActivity (new Intent (this, typeof(MainActivity)))), 5000);

		}
	}
}

