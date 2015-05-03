using System;
using SQLite.Net;

namespace RssReaderForms
{
	public interface ISQLite {
		SQLiteConnection GetConnection();
	}
}

