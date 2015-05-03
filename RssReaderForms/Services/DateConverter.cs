using System;
using Xamarin.Forms;

namespace RssReaderForms
{
	public class DateConverter:  IValueConverter
	{
		#region IValueConverter implementation
		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			string date = (string)value;
			return FeedsProcessor.GetParsedDate (date);
		}
		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}
		#endregion
	}
}

