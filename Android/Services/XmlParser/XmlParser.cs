using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Net;
using System.IO;
using System.ComponentModel;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[assembly:Dependency(typeof(RssReaderForms.Droid.XmlParser))]
namespace RssReaderForms.Droid
{
	
	public class XmlParser:IXmlParser
	{
		
		public  async Task<List<RssFeed>> GetFeeds()
		{
			List<RssFeed> feeds = new List<RssFeed> ();
			return await Task.Run (() => {
			
				using (WebClient client = new WebClient ()) {
					using (Stream rssFeedStream = client.OpenRead ("http://www.popmech.ru/out/public-all.xml")) {

						using (XmlReader reader = XmlReader.Create (rssFeedStream)) {

							reader.MoveToContent ();

							while (reader.ReadToFollowing ("item")) {
								var item =	ProcessItem (reader.ReadSubtree ());
								feeds.Add (item);
							}
						}
					}
				}
				return feeds;
			});
				
		}

		private RssFeed ProcessItem(XmlReader reader)
		{			
			reader.ReadToFollowing("title");
			string title = reader.ReadElementContentAsString("title", reader.NamespaceURI);

			reader.ReadToFollowing("link");
			string link = reader.ReadElementContentAsString("link", reader.NamespaceURI);

			reader.ReadToFollowing ("description");
			string description = reader.ReadElementContentAsString ("description", reader.NamespaceURI);

			string text = description.Substring (description.IndexOf ("<br />") + 6);

			reader.ReadToFollowing ("pubDate");
			string pubDate=reader.ReadElementContentAsString ("pubDate", reader.NamespaceURI);

			reader.ReadToFollowing ("enclosure");
			reader.MoveToFirstAttribute ();
			string enclosure = reader.GetAttribute ("url");

			return new RssFeed () {
				Title=title, Link=link,
				Description=description,
				PubDate=GetParsedDate(pubDate),
				Enclosure=enclosure,
				Text=text
			};

		}


		private string GetUrl(string text)
		{
			string regex = "http(.+)(jpg|JPG|png|PNG|gif|GIF|jpeg|JPEG)";
			Regex rgx = new Regex(regex, RegexOptions.IgnoreCase);
			MatchCollection matches = rgx.Matches(text);
			if (matches.Count > 0) {

				return matches [0].ToString();

			} else
				return "";

		}

		private string GetParsedDate(String date)
		{
			String workDate="";
			String regex = "\\d{2} ([a-zA-Z]{3}) \\d{4} \\d{2}\\:\\d{2}\\:\\d{2}";
			Regex rgx = new Regex(regex, RegexOptions.IgnoreCase);
			MatchCollection matches = rgx.Matches(date);
			if (matches.Count > 0) {

				workDate = matches [0].Value;

			} 
			DateTime dateTime=DateTime.Now;
			DateTime.TryParse (workDate, out dateTime);
			DateTime today = DateTime.Now;
			if (today.Day == dateTime.Day) {
				return "Today, " + dateTime.ToString ("HH:mm");
			} else if (today.Day == dateTime.Day + 1) {
				return "Yesterday, " + dateTime.ToString ("HH:mm");
			}

			return dateTime.ToString("dd.MM.yyyy HH:mm");
		}
	}
}

