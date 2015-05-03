using System;
using Xamarin.Forms.Platform.Android;

namespace RssReaderForms.Android
{
	public class ListButtonRenderer:ButtonRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged (e);
			if (Control != null) {
				Control.Focusable = false;
				Control.Clickable = false;
			}
		}
	}
}

