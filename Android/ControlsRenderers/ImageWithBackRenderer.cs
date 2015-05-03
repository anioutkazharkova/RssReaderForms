using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(RssReaderForms.ImageWithBack),typeof(RssReaderForms.Android.ImageWithBackRenderer))]
namespace RssReaderForms.Android
{
	public class ImageWithBackRenderer:ImageRenderer
	{
		public ImageWithBackRenderer ()
		{
		}
		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Image> e)
		{
			base.OnElementChanged (e);
			if (Control != null) {
				Control.SetBackgroundResource(Resource.Drawable.image_button_states);
				Control.Focusable = false;
				Control.Clickable = false;
			}
		}
	}
}

