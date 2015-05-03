using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace RssReaderForms
{
	public class FavoritePage:ContentPage
	{
		List<RssFeed> feeds = new List<RssFeed> ();
		List<RssFeed> temp = new List<RssFeed> ();
		ListView listView;
		IProgressDialog progressDialog = DependencyService.Get<IProgressDialog> ();
		int shift=0;

		public FavoritePage ()
		{
			this.Title = AppResources.Favorite;

			//this.Icon = Device.OnPlatform (null, "rss_icons.png", null);
			listView = new ListView () {				
				ItemTemplate = new DataTemplate (() => {
					//Сначала будет просто дата, текст с заголовком
					Label dateLabel = new Label () {
						HorizontalOptions = LayoutOptions.EndAndExpand,
						Font = Font.SystemFontOfSize (12),


					};
					//dateLabel.SetBinding (Label.TextProperty, "PubDate");
					dateLabel.SetBinding (Label.TextProperty, "PubDate",BindingMode.OneWay,new DateConverter());
					TitleLabel titleLabel = new TitleLabel () {
						VerticalOptions = LayoutOptions.StartAndExpand,
						HorizontalOptions = LayoutOptions.StartAndExpand,
						Font = Font.SystemFontOfSize (14),
						FontAttributes = FontAttributes.Bold
					};
					titleLabel.SetBinding (Label.TextProperty, "Title");
					TextLabel textLabel = new TextLabel () {
						VerticalOptions = LayoutOptions.StartAndExpand,
						HorizontalOptions = LayoutOptions.StartAndExpand,
						Font = Font.SystemFontOfSize (14),



					};
					textLabel.SetBinding (Label.TextProperty, "Text");

					StackLayout textLayout = new StackLayout {
						Orientation = StackOrientation.Vertical,
						Children = {
							titleLabel,
							textLabel
						}
					};

					ImageFeed feedPic = new ImageFeed {
						HeightRequest = 80,
						WidthRequest = 80,
						VerticalOptions = LayoutOptions.Center,


					};
					feedPic.SetBinding (Image.SourceProperty, "ImageBytes", BindingMode.OneWay, new ByteArrayToImageConverter ());

					return new ViewCell {
						View = new StackLayout {
							HorizontalOptions = LayoutOptions.StartAndExpand,
							VerticalOptions = LayoutOptions.StartAndExpand,
							Padding = new Thickness (5, 5, 5, 5),
							Orientation = StackOrientation.Vertical,
							Children = {
								dateLabel,

								new StackLayout {
									Orientation = StackOrientation.Horizontal,
									Children = {
										feedPic,
										textLayout
									}
								}


							}
						}
					};

				}),
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			listView.SeparatorColor = Color.Gray;

			listView.ItemSelected += async (sender, e) => {
				
					if (e.SelectedItem != null) {
						listView.SelectedItem = null;

						RssFeed feed = (RssFeed)e.SelectedItem;
						if (feed != null) {
							await this.Navigation.PushAsync (new CarouselFeedsPage (feeds, feeds.IndexOf (feed)));
							//this.Navigation.PushAsync (new FeedPage (feed));
						}
					}

			};


			listView.ItemAppearing += async (sender, e) => {

				if (feeds.Count > 10 && temp.Count < feeds.Count) {
					if (e.Item == temp [temp.Count - 1]) {
						
						progressDialog.ShowDialog ();
						temp = await LoadItems ();
						listView.ItemsSource = temp;

						progressDialog.DismissDialog ();
					} 
				}

			};
			listView.RowHeight = 120;
			StackLayout layout = new StackLayout {
				Children = {
					listView
				}
			};
			//UpdateImages ();
			this.Content = layout;
		}
		protected async override void OnAppearing ()
		{
			
			progressDialog.ShowDialog ();
			feeds = DatabaseWorker.GetFavorite ();

			//temp = new List<RssFeed> ();
			shift = (feeds.Count > 10) ? 10 : feeds.Count;

			temp = await LoadItems ();
			listView.ItemsSource = temp;

			progressDialog.DismissDialog ();

		}

		private async Task<List<RssFeed>> LoadItems ()
		{
			List<RssFeed> tempFeeds = new List<RssFeed> ();
			for (int i = 0; i < shift; i++) {
				if (feeds [i].ImageBytes == null) {
					byte[] imageBytes = null;
					try {
						using (var httpClient = new HttpClient ()) {
							imageBytes = await httpClient.GetByteArrayAsync (feeds [i].Enclosure);
							feeds [i].ImageBytes = imageBytes;
						}
					} catch (Exception e) {
					}
				}
				tempFeeds.Add (feeds [i]);
			}


			if (shift + 10 <= feeds.Count) {
				shift += 10;
			} else
				shift = feeds.Count;

			//	listView.ItemsSource = temp;
			return tempFeeds;

		}

	}
}

