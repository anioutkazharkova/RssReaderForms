using System;
using Android.OS;
using System.Net;
using System.Threading.Tasks;
using android=Android;

namespace RssReaderForms.Android
{
	public class ImageTask:AsyncTask
	{
		string url;

		protected  override Java.Lang.Object DoInBackground (params Java.Lang.Object[] @params)
		{
			android.OS.Process.SetThreadPriority (ThreadPriority.MoreFavorable);
			return  DownloadImage (url).Result;
		}

		private async Task<byte[]> DownloadImage (string url)
		{
			WebClient	webClient = new WebClient ();
			byte[] imageBytes = null;
			try{
				imageBytes =await webClient.DownloadDataTaskAsync(url);
			} catch(Exception e){
			}
			if (webClient != null) {
				webClient = null;
			}
			return imageBytes;
		}

		public ImageTask ()
		{
		}

		public ImageTask(string url)
		{
			this.url = url;
		}
	}
}

