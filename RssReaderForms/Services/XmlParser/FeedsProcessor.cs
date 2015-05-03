using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;

namespace RssReaderForms
{
	public class FeedsProcessor
	{
		public FeedsProcessor ()
		{
		}

		public static async Task<List<RssFeed>> LoadFeeds(string url)
		{
			
			try {
				var httpClient = new HttpClient ();
				var feed = url;
				var responseString = await httpClient.GetStringAsync (feed);

				var items = await GetFeeds (responseString);
				return items;

			} catch (Exception ex) {
				return null;
			}


		}

		public static async Task<List<RssFeed>> GetFeeds (string response)
		{
			List<RssFeed> feeds = new List<RssFeed> ();
			return await Task.Run (() => {
				var xdoc = XDocument.Parse (response);
				foreach (var item in xdoc.Descendants ("item"))
					{
					RssFeed feed= new RssFeed {
					Title = (string)item.Element ("title"),
					Description = (string)item.Element ("description"),
					Link = (string)item.Element ("link"),
					PubDate = (string)item.Element ("pubDate"),
					Enclosure = (string)item.Element ("enclosure").Attribute ("url"),
						Text=((string)item.Element ("description")).Substring (((string)item.Element ("description")).IndexOf ("<br />") + 6)
				};
						feeds.Add(feed);
					}
					return feeds;
			});

		}

		public static string GetParsedDate (String date)
		{
			String workDate = "";
			String regex = "\\d{2} ([a-zA-Z]{3}) \\d{4} \\d{2}\\:\\d{2}\\:\\d{2}";
			Regex rgx = new Regex (regex, RegexOptions.IgnoreCase);
			MatchCollection matches = rgx.Matches (date);
			if (matches.Count > 0) {

				workDate = matches [0].Value;

			} 
			DateTime dateTime = DateTime.Now;
			DateTime.TryParse (workDate, out dateTime);
			DateTime today = DateTime.Now;
			if (today.Day == dateTime.Day) {
				return AppResources.Today+"," + dateTime.ToString ("HH:mm");
			} else if (today.Day == dateTime.Day + 1) {
				return AppResources.Yesterday+ ", " + dateTime.ToString ("HH:mm");
			}

			return dateTime.ToString ("dd.MM.yyyy HH:mm");
		}
	}
}

