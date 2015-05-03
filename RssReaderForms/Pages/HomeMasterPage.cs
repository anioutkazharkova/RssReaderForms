using System;
using Xamarin.Forms;
namespace RssReaderForms
{
	public class HomeMasterPage:MasterDetailPage
	{
		MenuPage menu;
		string defaultUrl = "http://www.popmech.ru/out/public-all.xml";
		public HomeMasterPage ()
		{

			//DependencyService.Get<ISQLite> ().GetConnection ();
			//this.Icon = Device.OnPlatform (null, "rss_icons.png", null);
			 menu = new MenuPage ();
			menu.Menu.ItemSelected += (sender, e) => {
				NavigateTo (e.SelectedItem as MenuItem);
				//e.SelectedItem=null;
			};

			this.Master = menu;

			this.Detail = new NavigationPage(new FeedsListPage (defaultUrl)){
				BarTextColor=Color.White,
				Tint=Color.FromHex("#d35400")};
		}
		void NavigateTo (MenuItem menu)		{
			if (!menu.Title.Equals (AppResources.Favorite)) {
				var feedsPage = new FeedsListPage (menu.Url);
			
				Detail = new NavigationPage (feedsPage) {
					BarTextColor = Color.White,
					Tint = Color.FromHex ("#d35400")
				};

				IsPresented = false;
			} else {
				Detail= new NavigationPage (new FavoritePage()) {
					BarTextColor = Color.White,
					Tint = Color.FromHex ("#d35400")
				};
				IsPresented = false;
			}
		}



	}
}

