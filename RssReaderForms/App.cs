using System;
using Xamarin.Forms;


namespace RssReaderForms
{
	public class App
	{
		public static HomeMasterPage homePage;
		public static Page GetMainPage ()
		{	

			homePage= new HomeMasterPage ();
			return homePage;
		}
	}
}

