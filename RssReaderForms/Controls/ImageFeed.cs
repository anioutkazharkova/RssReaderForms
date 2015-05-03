using System;
using Xamarin.Forms;

namespace RssReaderForms
{
	public class ImageFeed:Image
	{
		public static readonly BindableProperty ImageUrlProperty = BindableProperty.Create<ImageFeed, string>(p => p.ImageUrl, default(string), BindingMode.OneWay);
		public static readonly BindableProperty ImageBytesProperty = BindableProperty.Create<ImageFeed,byte[]>(p => p.ImageBytes, default(byte[]), BindingMode.OneWay);
		public ImageFeed ()
		{
		}
		public string ImageUrl{
			get { return (string)base.GetValue( ImageUrlProperty ); }
			set { base.SetValue(ImageUrlProperty, value); }
		}
		public byte[] ImageBytes
		{
			get { return (byte[])base.GetValue( ImageBytesProperty ); }
			set { base.SetValue(ImageBytesProperty, value); }
		}
	}
}

