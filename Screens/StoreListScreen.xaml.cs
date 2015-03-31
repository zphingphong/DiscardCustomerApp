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

			MessagingCenter.Subscribe<AddStoreScreen, StoreObj> (this, "NewStoreAdded", (sender, newStore) => {
				stores.Add(newStore);
			});

			MessagingCenter.Subscribe<StoreCardScreen> (this, "FinishStoreCard", (sender) => {
				storeListView.SelectedItem = null;
			});
		}

		public async void goToAddStore (object sender, EventArgs e) {
			if (DependencyService.Get<IDeviceManager> ().IsDeviceOnline ()) {
				await Navigation.PushAsync (new AddStoreScreen ());
			} else {
				await DisplayAlert ("No Internet", "Please connect to the Internet to add new store.", "OK");
			}
		}

		public async void refreshStoreList (object sender, EventArgs e) {
			if (DependencyService.Get<IDeviceManager> ().IsDeviceOnline ()) {
				App.instance.userObj = App.instance.userManager.GetUpdatedUser ();
				App.instance.userManager.UpdateLocalUser (App.instance.userObj);
				App.instance.storeManager.GetNewStoresAssets (App.instance.userObj);
				foreach (StoreObj store in App.instance.userObj.stores) {
					store.uiXsLogoImagePath = App.instance.storeManager.xsLogoDirectoryPath + store._id + ".png";
				}
				storeListView.ItemsSource = App.instance.userObj.stores;
			} else {
				await DisplayAlert ("No Internet", "Please connect to the Internet to add new store.", "OK");
			}
		}
	}
}

