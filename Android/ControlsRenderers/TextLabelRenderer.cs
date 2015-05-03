using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using RssReaderForms;
using Android.Text;

[assembly: ExportRenderer(typeof(TextLabel),typeof(RssReaderForms.Android.TextLabelRenderer))]
namespace RssReaderForms.Android
{
	public class TextLabelRenderer:LabelRenderer
	{
		public TextLabelRenderer ()
		{
		}
		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Label> e)
		{
			base.OnElementChanged (e);
			if (Control != null) {
				Control.SetMaxLines (3);
				Control.SetLines (3);
				Control.Ellipsize = TextUtils.TruncateAt.End;
			}
		}
	}
}

