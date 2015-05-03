using System;
using Xamarin.Forms;
using System.IO;
using System.Globalization;
using System.Net.Http;

namespace RssReaderForms
{
	public class ByteArrayToImageConverter: IValueConverter
	{
		public  object Convert (object value, Type targetType, object parameter, CultureInfo culture)
		{
			byte[] imageAsBytes = (byte[])value;
			try{
			return ImageSource.FromStream (() => new MemoryStream (imageAsBytes));
			}
			catch(Exception e) {
				return ImageSource.FromFile(Device.OnPlatform(null,"gray.png",null));
			}
		
		}

		public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException ();
		}
	}


}

