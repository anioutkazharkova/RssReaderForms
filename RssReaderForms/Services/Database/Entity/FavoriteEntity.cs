using System;

namespace RssReaderForms
{
	public class FavoriteEntity:RssFeedEntity
	{
		public FavoriteEntity(){}
		public FavoriteEntity(RssFeedEntity item)
		{
			this.Title = item.Title;
			this.Text = item.Text;
			this.Link = item.Link;
			this.Description = item.Description;
			this.PubDate = item.PubDate;
			this.Enclosure = item.Enclosure;
			IsFavorite = item.IsFavorite;
		}
	}
}

