using System;
using Xamarin.Forms;
using android=Android;
using Android.Content;


[assembly: Dependency(typeof(RssReaderForms.Android.Sharing))]
namespace RssReaderForms.Android
{
	public class Sharing:IShareable
	{
		#region IShareable implementation

		public void ShareData (RssFeed feed)
		{
			var myIntent = new Intent(android.Content.Intent.ActionSend);      
			myIntent.SetType("text/plain");
			myIntent.AddFlags(ActivityFlags.ClearWhenTaskReset);

			// Add data to the intent, the receiving app will decide
			// what to do with it.
			myIntent.PutExtra(Intent.ExtraSubject, feed.Title);
			myIntent.PutExtra(Intent.ExtraText, feed.Link);
			Forms.Context.StartActivity(Intent.CreateChooser(myIntent,Forms.Context.Resources.GetString(Resource.String.Share)));
		}

		#endregion

		public Sharing ()
		{
		}
	}
}

