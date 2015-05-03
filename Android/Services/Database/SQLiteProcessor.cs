using System;
using SQLite.Net;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(RssReaderForms.Android.SQLiteProcessor))]
namespace RssReaderForms.Android
{
	public class SQLiteProcessor:ISQLite
	{
		public SQLiteProcessor ()
		{
		}

		#region ISQLite implementation

		public SQLiteConnection GetConnection ()
		{
			var fileName = "Posts.db";
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var path = Path.Combine (documentsPath, fileName);
		
			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid ();
			var connection = new SQLite.Net.SQLiteConnection (platform,path);

			return connection;
		}

		#endregion
	}
}

