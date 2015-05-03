using System;
using System.IO;
using System.Threading.Tasks;

namespace RssReaderForms
{
	public interface IImageDownloader
	{
		byte[] DownloadImage(string url);
	}
}

