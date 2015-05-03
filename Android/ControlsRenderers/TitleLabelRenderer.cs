using System;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using RssReaderForms;

[assembly: ExportRenderer(typeof(TitleLabel),typeof(RssReaderForms.Android.TitleLabelRenderer))]
namespace RssReaderForms.Android
{
	public class TitleLabelRenderer:LabelRenderer
	{
		public TitleLabelRenderer ()
		{
		}
		protected override void OnElementChanged (ElementChangedEventArgs<Xamarin.Forms.Label> e)
		{
			base.OnElementChanged (e);
			if (Control != null) {
				Control.SetMaxLines (2);
				Control.SetLines (2);

			}
		}
	}
}