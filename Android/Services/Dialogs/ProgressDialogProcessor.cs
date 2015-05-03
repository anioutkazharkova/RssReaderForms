using System;
using Android.App;
using android=Android;
using Xamarin.Forms;
[assembly: Dependency(typeof(RssReaderForms.Android.ProgressDialogProcessor))]
namespace RssReaderForms.Android
{
	public class ProgressDialogProcessor:IProgressDialog
	{
		public static ProgressDialog mDialog = new ProgressDialog (Forms.Context){Indeterminate=true};
		public ProgressDialogProcessor ()
		{
			
		}

		#region IProgressDialog implementation

		public void ShowDialog ()
		{
			try
			{
				mDialog.SetMessage("Loading...");
				mDialog.SetCancelable(true); 
				mDialog.Show();

				//return true; 
			}
			catch (Exception e)
			{
				Console.WriteLine("Show HUD error: " + e.Message);
				throw e;
			}
		}

		public void DismissDialog ()
		{
			if (mDialog != null) {
				try{
					mDialog.Dismiss();
				}
				catch(Exception e) {
				}
			}
		}

		#endregion
	}
}

