using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RssReaderForms
{
	public class CarouselFeedsPage:CarouselPage
	{
		List<RssFeed> list = new List<RssFeed> ();
		int position = 0;
		List<RssFeed> temp=new List<RssFeed>();

		int shift=0;
		int portion=10;
		int currentPosition=0;
		int lastPosition=0;

		int left=0;
		int right = 0;

		/// <summary>
		/// Ставим 10 страниц из 10ки, содержащей искомую позицию
		/// При листании вправо на крайней добавляем еще 10, определяем крайнюю позицию.
		/// При листании влево на крайней добавляем еще 5, определяем крайнюю позицию
		/// </summary>

		public CarouselFeedsPage ()
		{
			shift=0;
			portion=10;
			currentPosition=0;
			lastPosition=0;

			left=0;
			right = 0;
		}

		public CarouselFeedsPage (List<RssFeed> feeds, int position=0):this()
		{
			
			this.Icon = Device.OnPlatform (null, "rss_icons.png", null);
			if (feeds != null)
				list = new List<RssFeed> (feeds);
			this.position = position;

			this.CurrentPageChanged += async (sender, e) => {


				int index=this.Children.IndexOf(CurrentPage);
				currentPosition=index;
				this.Title=string.Format("Feed: {0}/{1}",index+1+left,list.Count);
				if (currentPosition>lastPosition)
				{
					if (index==temp.Count-1 && index<list.Count-10)
					{
						if (shift + 10 <= list.Count) {
							shift += 10;
						} else {
							shift = list.Count;
						}
						right=shift;

						for (int i = index+1; i < shift; i++) {

							temp.Add (list [i]);
							await 
							Task.Delay(200);
							Children.Add (new FeedPage (list[i],i));

						}

					}
				}
				else if (currentPosition<lastPosition)
				{
					if (index==0 && left-5>=0)
					{
						//Добавить 10
						for(int i=0;i<5;i++)
						{
							temp.Add (list [left-1]);
							await 
							Task.Delay(200);
							Children.Insert (0,new FeedPage (list[left-1],left-1));
							left--;
						}

					}
				}

				lastPosition=currentPosition;

			};

			Init ();
		}

		private async void Init ()
		{
			int start = ((position + 1) / portion) * portion;
			if (start - 5 >= 0)
				start -= 5;
			else
				start = 0;
			left = start;
			shift = (list.Count > 15+left) ?15+left : list.Count;
			right = shift;
			for (int i = left; i <shift; i++) {
				temp.Add (list [i]);					
				await AddChild (i);


			}
			this.Title=string.Format("Feed: {0}/{1}",1+left,list.Count);
		}
		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
		
			if (position-left< this.Children.Count) {
				await Task.Delay (50);

				this.CurrentPage = (FeedPage)this.Children [position-left];
			}
		}

		async Task AddChild(int ind)
		{
			Children.Add (new FeedPage (list [ind], ind));
		}
	}
}

