using System;
using System.Collections.Generic;

namespace RssReaderForms
{
	public class DatabaseWorker
	{
		static	FeedsDatabase dbWorker=new FeedsDatabase();

		public static List<RssFeed> GetAllItems()
		{
			if (dbWorker == null)
				dbWorker = new FeedsDatabase ();
			return dbWorker.GetAllItems ();
		}

		public static void SaveAllItems(List<RssFeed> list)
		{
			if (dbWorker == null)
				dbWorker = new FeedsDatabase ();

			dbWorker.SaveAllItems (list);
		}

		public static List<RssFeed> GetAllItems(string category)
		{
			if (dbWorker == null)
				dbWorker = new FeedsDatabase ();
			return dbWorker.GetAllItems (category);
		}

		public static List<RssFeed> GetFavorite()
		{
			if (dbWorker == null)
				dbWorker = new FeedsDatabase ();
			return dbWorker.GetFavorite ();
		}

		public static List<RssFeed> SaveAllItems(List<RssFeed> list,string category)
		{
			if (dbWorker == null)
				dbWorker = new FeedsDatabase ();

			return dbWorker.SaveAllItems (list,category);
		}

		public static bool UpdateItem(string url)
		{
			if (dbWorker == null)
				dbWorker = new FeedsDatabase ();
			
			return dbWorker.UpdateItemFavorite (url);
		}

		public static bool CleanCache()
		{
			if (dbWorker == null)
				dbWorker = new FeedsDatabase ();
			
			return dbWorker.CleanCache ();
		}
		public static bool CleanCache(string category)
		{
			if (dbWorker == null)
				dbWorker = new FeedsDatabase ();

			return dbWorker.CleanCacheByCategory (category);
		}


	}
}

