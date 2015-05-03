using System;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace RssReaderForms
{
	public class FeedsListPage:ContentPage
	{
		List<RssFeed> feeds = new List<RssFeed> ();
		List<RssFeed> temp = new List<RssFeed> ();
		ListView listView;
		int shift = 10;
		bool startRefreshing = false;
		bool isLoading = false;
		IProgressDialog progressDialog = DependencyService.Get<IProgressDialog> ();

		string url = "";
		string defaultUrl = "http://www.popmech.ru/out/public-all.xml";

		public  FeedsListPage (string url)
		{			

			this.Title = "Rss Reader";
			this.url = url;
			//this.Icon = Device.OnPlatform (null, "rss_icons.png", null);
			listView = new ListView () {
				IsPullToRefreshEnabled = true,
				IsRefreshing = startRefreshing,
				RefreshCommand = LoadFeedsCommand,
				ItemTemplate = new DataTemplate (() => {
					//Сначала будет просто дата, текст с заголовком
					Label dateLabel = new Label () {
						HorizontalOptions = LayoutOptions.EndAndExpand,
						Font = Font.SystemFontOfSize (12),


					};
					ImageWithBack favButton=new ImageWithBack(){
						Source = Device.OnPlatform (null, "not_favorite_grey", null),
						WidthRequest = 20,
						HeightRequest = 20,
						HorizontalOptions = LayoutOptions.StartAndExpand,
						VerticalOptions = LayoutOptions.Center
					};
					dateLabel.SetBinding (Label.TextProperty, "PubDate",BindingMode.OneWay,new DateConverter());
					favButton.SetBinding(Image.SourceProperty,"IsFavorite",BindingMode.OneWay,new FavImageConverter());
					favButton.SetBinding(ImageWithBack.ImageTagProperty,"Id");

					favButton.GestureRecognizers.Add(new TapGestureRecognizer(async(view)=>{
						//this.DisplayAlert("","test","OK");
						int position=(favButton.ImageTag)%(feeds.Count);
						AddRemoveToFavorite(feeds[position]);
						if (feeds[position].IsFavorite)
						{
							favButton.Source=ImageSource.FromFile(Device.OnPlatform (null, "favorite_grey.png", null));
						}
						else
						{
							favButton.Source=ImageSource.FromFile(Device.OnPlatform (null, "not_favorite_grey.png", null));
						}
						//this.DisplayAlert("","test:" +position,"OK");
					}));

					StackLayout dateFav=new StackLayout()
					{
						VerticalOptions = LayoutOptions.StartAndExpand,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						Orientation=StackOrientation.Horizontal,
						Children={
							favButton,
							dateLabel}
							,
						HeightRequest=20
					};

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
								dateFav,

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
				if (!isLoading) {
					if (e.SelectedItem != null) {
						listView.SelectedItem = null;

						RssFeed feed = (RssFeed)e.SelectedItem;
						if (feed != null) {
							await this.Navigation.PushAsync (new CarouselFeedsPage (feeds, feeds.IndexOf (feed)));
							//this.Navigation.PushAsync (new FeedPage (feed));
						}
					}
				}
			};


			listView.ItemAppearing += async (sender, e) => {
				
				if (feeds.Count > 10 && temp.Count < feeds.Count) {
					if (e.Item == temp [temp.Count - 1]) {
						isLoading = true;
						progressDialog.ShowDialog ();
						temp = await LoadItems ();
						listView.ItemsSource = temp;
						isLoading = false;
						progressDialog.DismissDialog ();
					} 
				}

			};
			listView.RowHeight = 125;
			StackLayout layout = new StackLayout {
				Children = {
					listView
				}
			};
			//UpdateImages ();
			this.Content = layout;
			//UpdateImages ();
		}

		protected async override void OnAppearing ()
		{
			//feeds = new List<RssFeed> ();
			isLoading = true;
			progressDialog.ShowDialog ();
			feeds = DatabaseWorker.GetAllItems (url);
			if (feeds == null || feeds.Count == 0) {
				feeds = await FeedsProcessor.LoadFeeds (url);
				feeds=DatabaseWorker.SaveAllItems (feeds,url);
			}

			//temp = new List<RssFeed> ();
			shift = (feeds.Count > 10) ? 10 : feeds.Count;

			temp = await LoadItems ();
			listView.ItemsSource = temp;
			isLoading = false;
			progressDialog.DismissDialog ();

		}
		void AddRemoveToFavorite(RssFeed feed)
		{

			feed.IsFavorite = !feed.IsFavorite;
			DatabaseWorker.UpdateItem (feed.Link);
			/*if (feed.IsFavorite) {
				addToFavorite.Icon = Device.OnPlatform (null, "favorite_white.png", null);
			} else {
				addToFavorite.Icon = Device.OnPlatform (null, "not_favorite_white.png", null);
			}*/
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


		public Command LoadFeedsCommand {
			get { 
				return  new Command (async() => {
					ExecuteLoadFeedsCommand ();
				});
					
			}
		}

		private async void ExecuteLoadFeedsCommand ()
		{

			await LoadData ();
		}

		private async Task LoadData ()
		{
			if (startRefreshing)
				return;

			startRefreshing = true;	

			feeds = await FeedsProcessor.LoadFeeds (url);
			feeds=DatabaseWorker.SaveAllItems (feeds,url);

			temp = new List<RssFeed> ();
			shift = (feeds.Count > 10) ? 10 : feeds.Count;

			temp = await LoadItems ();		
			listView.ItemsSource = temp;
			startRefreshing = false;
			listView.IsRefreshing = false;
						
		}
		class FavImageConverter:IValueConverter
		{
			#region IValueConverter implementation
			public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
			{
				bool isFav = (bool)value;
				if (isFav)
					return ImageSource.FromFile (Device.OnPlatform (null,"favorite_grey.png",null));
				else
					return ImageSource.FromFile (Device.OnPlatform (null,"not_favorite_grey.png",null));
			}
			public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
			{
				throw new NotImplementedException ();
			}
			#endregion
		}

	}

}

