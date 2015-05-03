using System;
using SQLite.Net.Attributes;

namespace RssReaderForms
{
	public class RssFeedEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Title{ get; set;}
		public string Link{ get; set; }
		public string Description{ get; set; }
		public string PubDate{ get; set;}
		public string Enclosure{get;set;}
		public string Text{ get; set;}
		public string Category{get;set;}
	
		public bool IsFavorite{ get; set;}


		public RssFeedEntity(){}
		public RssFeedEntity(RssFeed item)
		{
			this.Title = item.Title;
			this.Text = item.Text;
			this.Link = item.Link;
			this.Description = item.Description;
			this.PubDate = item.PubDate;
			this.Enclosure = item.Enclosure;
			IsFavorite = item.IsFavorite;
		}

		public RssFeed ToRssFeed()
		{
			return new RssFeed () {
				Title=Title,
				Text=Text,
				Description=Description,
				Link=Link,
				Enclosure=Enclosure,
				PubDate=PubDate,
				IsFavorite=IsFavorite
				
			};
		}
	}
}

