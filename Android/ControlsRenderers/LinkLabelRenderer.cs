using System;
using Xamarin.Forms;
using RssReaderForms;
using Xamarin.Forms.Platform.Android;
using Android.Text;
using Android.Widget;


[assembly: ExportRenderer(typeof(LinkLabel),typeof(RssReaderForms.Android.LinkLabelRenderer))]
namespace RssReaderForms.Android
{
	public class LinkLabelRenderer:LabelRenderer
	{
		public LinkLabelRenderer ()
		{

		}
		protected override void OnElementChanged (ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged (e);
			if (Control != null) {
				Control.AutoLinkMask = global::Android.Text.Util.MatchOptions.WebUrls;
				Control.SetText(Html.FromHtml(string.Format ("<a href=\"{0}\">Link</a>",Control.Text)),TextView.BufferType.Spannable);
			}
		}

	}
}

