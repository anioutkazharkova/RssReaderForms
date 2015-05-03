using System;
using Xamarin.Forms;
using SQLite.Net;
using System.Collections.Generic;
using System.Linq;

namespace RssReaderForms
{
	public class FeedsDatabase
	{
		static object locker = new object ();
		SQLiteConnection database;

		public FeedsDatabase ()
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();
			database.CreateTable<RssFeedEntity> ();
			database.CreateTable<CategoryEntity> ();
			database.CreateTable<FavoriteEntity> ();
		}

		public IEnumerable<RssFeedEntity> GetItems ()
		{
			lock (locker) {
				return (from i in database.Table<RssFeedEntity> ()
				        select i).ToList ();
			}
		}

		public IEnumerable<RssFeedEntity> GetItemsByCategory (string category)
		{
			lock (locker) {
				return (from i in database.Table<RssFeedEntity> ()
				        where i.Category.Equals (category)
				        select i).ToList ();
			}
		}

		public List<RssFeed> GetAllItems ()
		{
			List<RssFeed> list = new List<RssFeed> ();
			var items = GetItems ();
			foreach (var item in items) {
				list.Add (item.ToRssFeed ());
			}
			return list;
		}

		public List<RssFeed> GetAllItems (string category)
		{
			List<RssFeed> list = new List<RssFeed> ();
			var items = GetItemsByCategory (category);
			foreach (var item in items) {
				list.Add (item.ToRssFeed ());
			}
			return list;
		}

		public void SaveAllItems (List<RssFeed> list)
		{
			int index = -1;
			List<RssFeed> prevPosts = GetAllItems ();
			if (prevPosts != null && prevPosts.Count > 0) {
				index = GetIndexByUrl(list,prevPosts [0].Link);
				if (index > -1) {
					for (int i = index - 1; i >= 0; i--) {
						prevPosts.Insert (0, list [i]);
					}
				} else
					prevPosts = list;						


			} else
				prevPosts = list;
			CleanCache ();
			foreach (var item in prevPosts) {
				RssFeedEntity feed = new RssFeedEntity (item);
				SaveItem (feed);
			}
		}

		private int GetIndexByUrl(List<RssFeed> list, string url)
		{int index = -1;
			for (int i = 0; i < list.Count; i++)
				if (list [i].Link.Equals (url)) {
					index = i;
				}
			return index;
		}

		public List<RssFeed> SaveAllItems (List<RssFeed> list, string category)
		{
			int index = -1;
			List<RssFeed> prevPosts = GetAllItems (category);
			if (prevPosts != null && prevPosts.Count > 0) {
				index = GetIndexByUrl(list,prevPosts [0].Link);
				if (index > 0) {
					for (int i = index - 1; i >= 0; i--) {
						prevPosts.Insert (0, list [i]);
					}
				} else
					if (index==-1)
					prevPosts = list;						


			} else
				prevPosts = list;
			CleanCacheByCategory (category);
			foreach (var item in prevPosts) {
				RssFeedEntity feed = new RssFeedEntity (item);
				feed.Category = category;
				SaveItem (feed);
			}
			return prevPosts;
		}


		public bool CleanCache ()
		{
			lock (locker) {
				database.DeleteAll<RssFeedEntity> ();
			}
			return true;
		}

		public bool CleanCacheByCategory (string category)
		{
			var items = GetItemsByCategory (category);
			lock (locker) {
				if (items != null && items.Count () > 0) {
					foreach (var item in items) {
						database.Delete (item);
					}
				}
				
			}
			return true;
		}

		public List<RssFeed> GetFavorite ()
		{
			List<RssFeed> list = new List<RssFeed> ();
			lock (locker) {
				var items = database.Table<FavoriteEntity> ().Where (x => x.IsFavorite == true).ToList ();
				foreach (var item in items) {
					list.Add (item.ToRssFeed ());
				}
			}
			return list;
		}

		private bool AddRemoveFavorite (RssFeedEntity item)
		{
			lock (locker) {
				if (item.IsFavorite) {
					database.Insert (new FavoriteEntity (item));
				} else {
					database.Delete (GetFavoriteItem (item.Link));
				}
			}
			return true;
		}

		public FavoriteEntity GetFavoriteItem (string url)
		{
			lock (locker) {
				return database.Table<FavoriteEntity> ().FirstOrDefault (x => x.Link == url);
			}
		}

		public RssFeedEntity GetItem (string url)
		{
			lock (locker) {
				return database.Table<RssFeedEntity> ().FirstOrDefault (x => x.Link == url);
			}
		}

		public bool UpdateItemFavorite (string url)
		{
			RssFeedEntity item = GetItem (url);
			item.IsFavorite = !item.IsFavorite;
			AddRemoveFavorite (item);
			SaveItem (item);
			return true;
		}

		public int SaveItem (RssFeedEntity item)
		{
			lock (locker) {
				if (item.Id != 0) {
					database.Update (item);
					return item.Id;
				} else {
					return database.Insert (item);
				}
			}
		}
		public int SaveCategory(CategoryEntity item)
		{
			lock (locker) {
			
			}
			return 0;
		}

	}
}

