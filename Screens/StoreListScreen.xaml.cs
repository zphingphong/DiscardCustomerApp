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

			storeListView.ItemTapped += async (sender, e) => {
				StoreCardScreen storeCardScreen = new StoreCardScreen ((StoreObj)storeListView.SelectedItem);
				await Navigation.PushAsync (storeCardScreen);
			};

//			MessagingCenter.Subscribe<AddStoreScreen, StoreObj> (this, "NewStoreAdded", (sender, newStore) => {
//				stores.Add(newStore);
//			});

			MessagingCenter.Subscribe<StoreCardScreen> (this, "FinishStoreCard", (sender) => {
				storeListView.SelectedItem = null;
			});
		}

		public async void goToAddStore (object sender, EventArgs e) {
			await Navigation.PushAsync (new AddStoreScreen ());
		}
	}
}

