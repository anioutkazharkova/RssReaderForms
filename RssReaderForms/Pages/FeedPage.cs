using System;
using Xamarin.Forms;
using System.IO;
using System.Net.Http;

namespace RssReaderForms
{
	public class FeedPage:ContentPage
	{
		RssFeed feed;
		StackLayout layout;
		Image image;
		ScrollView scrollView;
		ToolbarItem addToFavorite ;
		int id=0;
		public int Id{get{ return id;}}

		public FeedPage (RssFeed feed,int id)
		{
			this.feed = feed;
			this.Title = "Rss Feed";
			this.id = id;
			this.Icon = Device.OnPlatform (null, "rss_icons.png", null);


			//ms= DependencyService.Get<IImageDownloader>().DownloadImage(feed.ImageUrl).Result;

			layout = new StackLayout ()
			{ 
				Padding=new Thickness(20,10,20,10),
				Orientation=StackOrientation.Vertical,
				VerticalOptions=LayoutOptions.StartAndExpand
			};
			Label dateLabel = new Label () {
				HorizontalOptions=LayoutOptions.EndAndExpand,
				Text=feed.PubDate,
				FontSize=12
			};

			Label titleLabel = new Label () {
				HorizontalOptions=LayoutOptions.StartAndExpand,
				Text=feed.Title,
				FontSize=18,
				FontAttributes=FontAttributes.Bold
			};

			image = new Image () {
				VerticalOptions=LayoutOptions.Center,
				HorizontalOptions=LayoutOptions.Center,
				HeightRequest=250,
				WidthRequest=250,

			};




			addToFavorite = new ToolbarItem () {
				Text=AppResources.AddToFavorite,
				Icon=Device.OnPlatform(null,"not_favorite_white.png",null),
				Order=ToolbarItemOrder.Primary
			};
			if (feed.IsFavorite) {
				addToFavorite.Icon = Device.OnPlatform (null, "favorite_white.png", null);
			} else {
				addToFavorite.Icon = Device.OnPlatform (null, "not_favorite_white.png", null);
			}
			ToolbarItem share = new ToolbarItem () {
				Text=AppResources.Share,
				Icon=Device.OnPlatform(null,"ic_action_share.png",null),
				Order=ToolbarItemOrder.Primary
			};

			addToFavorite.Activated += (sender, e) => {
				AddRemoveToFavorite();
			};
			share.Activated+=(async (sender, e) => 
				{
					if (feed!=null)
					{
					DependencyService.Get<IShareable>().ShareData(feed);
					}
				});
			this.ToolbarItems.Add (addToFavorite);
			this.ToolbarItems.Add (share);

			Label textLabel = new Label () {
				Text=feed.Text,
				FontSize=16
			};
			FormattedString fs = new FormattedString ();
			fs.Spans.Add (new Span (){ForegroundColor=Color.Blue,Text=AppResources.Go_to_link,FontAttributes=FontAttributes.Bold});

			Label linkLabel = new Label () {
				FormattedText=fs,
				FontSize=14,
				HorizontalOptions=LayoutOptions.EndAndExpand
			};

			linkLabel.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked()));

			layout.Children.Add (titleLabel);
			layout.Children.Add (dateLabel);
			layout.Children.Add (image);
			layout.Children.Add (textLabel);
			layout.Children.Add (linkLabel);

			scrollView = new ScrollView () {
				VerticalOptions=LayoutOptions.FillAndExpand,
				Content=layout
			};

			this.Content =scrollView;
			scrollView.IsVisible = false;
		}

		void AddRemoveToFavorite()
		{
			
			feed.IsFavorite = !feed.IsFavorite;
			DatabaseWorker.UpdateItem (feed.Link);
			if (feed.IsFavorite) {
				addToFavorite.Icon = Device.OnPlatform (null, "favorite_white.png", null);
			} else {
				addToFavorite.Icon = Device.OnPlatform (null, "not_favorite_white.png", null);
			}
		}

		protected async override void OnAppearing ()
		{
			base.OnAppearing ();
			byte[] imageBytes = null;
			try{
				using (var httpClient = new HttpClient ()) {
					imageBytes = await httpClient.GetByteArrayAsync (feed.Enclosure);
				}
				if (imageBytes!=null)
					image.Source = ImageSource.FromStream (() => new MemoryStream(imageBytes));
			}
			catch(Exception e)
			{
			}
			scrollView.IsVisible = true;
		}

		void OnLabelClicked ()
		{
			if (feed != null) {
				Device.OpenUri (new Uri (feed.Link));
			}
		}
	}
}

