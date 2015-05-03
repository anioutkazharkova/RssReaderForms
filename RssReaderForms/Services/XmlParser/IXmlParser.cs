using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RssReaderForms
{
	public interface IXmlParser
	{
		Task<List<RssFeed>> GetFeeds();
	}
}

