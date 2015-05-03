using System;
using Xamarin.Forms;

namespace RssReaderForms
{
	public class MenuPage:ContentPage
	{
		ListView listView;
		public ListView Menu{ get { return listView; } }
		ImageWithBack imageButton;
		public ImageWithBack ImageButton{get{return imageButton; }}
		public MenuPage ()
		{
			//this.Icon = Device.OnPlatform (null, "rss_icons.png", null);
			this.Title="RSS reader";
			MenuItem[] items = new MenuItem[] {
				new MenuItem (){ Title = AppResources.Favorite },
				new MenuItem () {
					Title = "Popmech",
					Url = "http://www.popmech.ru/out/public-all.xml"
				}
			};
			listView = new ListView () {
				ItemsSource=items,
				ItemTemplate=new DataTemplate(()=>
				{
						Label titleLabel=new Label(){
							FontSize=14,
							VerticalOptions=LayoutOptions.Center,
							HorizontalOptions=LayoutOptions.Start
						};
						titleLabel.SetBinding (Label.TextProperty, "Title");
						return new ViewCell{
							View=titleLabel
						};

					})
			};
			imageButton = new ImageWithBack () {
				Source = Device.OnPlatform (null, "ic_action_new.png", null),
				WidthRequest = 30,
				HeightRequest = 30,
				HorizontalOptions = LayoutOptions.EndAndExpand,
				VerticalOptions = LayoutOptions.Center
			};
			imageButton.GestureRecognizers.Add(new TapGestureRecognizer(async(view) => OnAddClicked()));


			StackLayout horLayout = new StackLayout {
				Orientation=StackOrientation.Horizontal,
				Padding=new Thickness(0,5,5,0),
				Children={
					new Label(){
						Text=AppResources.AddNewChannel,
						HorizontalOptions=LayoutOptions.StartAndExpand,
						VerticalOptions=LayoutOptions.Center
					},
					imageButton

				}
			};

			this.Content = new StackLayout {
				Padding=new Thickness(10,0,0,10),
				Orientation=StackOrientation.Vertical,
				Children={
					//horLayout,
					listView
				}
			};
		}
		private	async void OnAddClicked()
		{
			//this.Navigation.PushAsync(new NavigationPage(new ChannelPage()));
			/*
			Detail = new NavigationPage (new ChannelPage()) {
				BarTextColor = Color.White,
				Tint = Color.FromHex ("#d35400")
			};
			IsPresented = false;*/
		}

	}



}

