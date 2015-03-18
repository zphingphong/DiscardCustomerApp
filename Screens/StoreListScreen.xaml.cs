using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class StoreListScreen : ContentPage {
		public StoreListScreen (List<StoreObj> stores, StoreManager storeManager) {
			InitializeComponent ();
			foreach (StoreObj store in stores) {
				store.uiXsLogoImagePath = storeManager.xsLogoDirectoryPath + store._id + ".png";
			}
			storeListView.ItemsSource = stores;
			this.BindingContext = storeManager;

			storeListView.ItemSelected += async (sender, e) => {
//				var todoItem = (TodoItem)e.SelectedItem;
				await Navigation.PushAsync (new StoreCardScreen ((StoreObj)e.SelectedItem));
			};
		}

		public async void goToAddStore (object sender, EventArgs e) {
			await Navigation.PushAsync (new AddStoreScreen ());
		}
	}
}

