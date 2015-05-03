using System;
using System.IO;
using Xamarin.Forms;
using Android.OS;
using System.Net;
using System.Threading.Tasks;

[assembly: Dependency(typeof(RssReaderForms.Android.ImageDownloader))]
namespace RssReaderForms.Android
{
	public class ImageDownloader:IImageDownloader
	{


		/*public async Task<MemoryStream> DownloadImage (string url)
		{
		WebClient	webClient = new WebClient ();
			byte[] imageBytes = null;
			try{
				imageBytes = await webClient.DownloadDataTaskAsync(url);
			} catch(Exception e){
			}

			return (imageBytes!=null)?new MemoryStream(imageBytes):null;
		}*/
		public byte[] DownloadImage(string url)
		{
			return (byte[])new ImageTask (url).ExecuteOnExecutor (AsyncTask.ThreadPoolExecutor).GetAsync().Result;
		}


	}
}

