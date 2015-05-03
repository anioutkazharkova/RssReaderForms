using System;
using Xamarin.Forms;

namespace RssReaderForms
{
	public class ChannelPage:ContentPage
	{
		MenuItem channel=new MenuItem();
		bool isSaved=false;

		public ChannelPage ()
		{
			this.BindingContext = channel;
			this.Title=AppResources.NewChannel;
			Entry channelName = new Entry () {
				HorizontalOptions=LayoutOptions.FillAndExpand,
				VerticalOptions=LayoutOptions.Start,

			};
			Entry channelUrl = new Entry () {
				HorizontalOptions=LayoutOptions.FillAndExpand,
				VerticalOptions=LayoutOptions.Start,

			};
			channelName.TextChanged += ((sender, e) => {
				channel.Title=e.NewTextValue;
			});
			channelUrl.TextChanged += (sender, e) => {
				channel.Url=e.NewTextValue;
			};

			ToolbarItem saveItem = new ToolbarItem () {
				Icon=Device.OnPlatform(null,"ic_action_accept.png",null)
			};
			saveItem.Activated += async (sender, e) => {
				if (isSaved)
				{
					//Создаем канал

					//закрываем вкладку

				}
			};

			this.ToolbarItems.Add (saveItem);

			Button saveButton = new Button () {
				VerticalOptions=LayoutOptions.FillAndExpand,
				HorizontalOptions=LayoutOptions.FillAndExpand,
				Text=AppResources.Save
			};

			saveButton.Clicked += async (sender, e) => {

				//Создаем новый канал

			};
			Button cancelButton = new Button () {
				VerticalOptions=LayoutOptions.FillAndExpand,
				HorizontalOptions=LayoutOptions.FillAndExpand,
				Text=AppResources.Cancel
			};
			cancelButton.Clicked += async (sender, e) => {
				//await	this.Navigation.PopAsync();

			};

			StackLayout layout = new StackLayout {
				VerticalOptions=LayoutOptions.FillAndExpand,
				HorizontalOptions=LayoutOptions.FillAndExpand,
				Padding=new Thickness(20,30,20,20),
				Children={
					new Label()
					{
						VerticalOptions=LayoutOptions.Start,
						Text="Name"
					},
					channelName,
					new Label()
					{
						VerticalOptions=LayoutOptions.Start,
						Text="Url"
					},
					channelUrl,
					new StackLayout()
					{
						Orientation=StackOrientation.Horizontal,
						HeightRequest=50,
						Children={
							//saveButton,
							//cancelButton
						}
					}
				}
			};
			this.Content = layout;
		}
	}
}

