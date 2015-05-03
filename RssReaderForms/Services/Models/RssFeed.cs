using System;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RssReaderForms
{
	public class RssFeed:INotifyPropertyChanged
	{
		static int i=-1;
		public int Id{ get; set;}
		public string Title{ get; set;}
		public string Link{ get; set; }
		public string Description{ get; set; }
		public string PubDate{ get; set;}
		public string Enclosure{get;set;}
		public bool IsFavorite{ get; set;}
		public string ComplexContent{get{return Title + " " + Text; }}

		public string Text{ get; set;}
		public byte[] ImageBytes{ get; set;}
		private Stream imageStream;
		public Stream ImageStream{ get { return imageStream; }  set { imageStream = value; OnPropertyChanged("ImageStream"); } }

		public RssFeed()
		{
			Id = ++i;
		}
		public override string ToString ()
		{
			return string.Format ("[RssFeed: Title={0}, Link={1}, Description={2}, PubDate={3}, Enclosure={4}]", Title, Link, Description, PubDate, Enclosure);
		}

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;
		#endregion
		void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

