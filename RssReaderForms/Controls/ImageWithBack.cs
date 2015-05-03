using System;
using Xamarin.Forms;

namespace RssReaderForms
{
	public class ImageWithBack:Image
	{
		public static readonly BindableProperty ImageTagProperty = BindableProperty.Create<ImageWithBack,int>(p => p.ImageTag, default(int), BindingMode.OneWay);
		public ImageWithBack ()
		{
		}
		public int ImageTag{
			get { return (int)base.GetValue( ImageTagProperty ); }
			set { base.SetValue(ImageTagProperty, value); }
		}
	}
}

