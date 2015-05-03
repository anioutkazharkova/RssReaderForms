using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using RssReaderForms;
using Android.Graphics;
using Java.Util.Logging;
using Android.Widget;
using Android.OS;

[assembly: ExportRenderer(typeof(ImageFeed),typeof(RssReaderForms.Android.ImageFeedRenderer))]
namespace RssReaderForms.Android
{
	public class ImageFeedRenderer:ImageRenderer
	{
		public ImageFeedRenderer ()
		{
		}
		protected async override void OnElementChanged (ElementChangedEventArgs<Image> e)
		{
			base.OnElementChanged (e);


			/*byte[] imageBytes = null;
			try
			{
				imageBytes=(byte[])(new ImageTask(((ImageFeed)this.Element).ImageUrl).ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor)).GetAsync().Result;
			}
			catch(Exception ex)
			{
			}
				if (imageBytes != null) {
					Bitmap bitmap = await BitmapFactory.DecodeByteArrayAsync (imageBytes, 0, imageBytes.Length);
				if (bitmap != null) {
					Control.SetImageBitmap (bitmap);
					bitmap = null;
				} else
					Control.SetImageDrawable (null);
				imageBytes = null;
				}
			}
			*/



		}}
}

