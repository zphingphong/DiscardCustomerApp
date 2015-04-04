using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class StoreListScreen : ContentPage {
		public StoreListScreen (List<StoreObj> stores, StoreManager storeManager) {
			InitializeComponent ();
			this.PrepareStoreLogo (stores, storeManager);
			storeListView.ItemsSource = stores;
			this.BindingContext = storeManager;

			storeListView.ItemTapped += async (sender, e) => {
				StoreCardScreen storeCardScreen = new StoreCardScreen ((StoreObj)storeListView.SelectedItem);
				await Navigation.PushAsync (storeCardScreen);
			};

			MessagingCenter.Subscribe<AddStoreScreen, StoreObj> (this, "NewStoreAdded", (sender, newStore) => {
				this.PrepareStoreLogo (App.instance.userObj.stores, storeManager);
				storeListView.ItemsSource = App.instance.userObj.stores;
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
			loadingMask.IsRunning = true;
			if (DependencyService.Get<IDeviceManager> ().IsDeviceOnline ()) {
				App.instance.userObj = await App.instance.userManager.GetUpdatedUserTaskAsync ();
				App.instance.userManager.UpdateLocalUser (App.instance.userObj);
				App.instance.storeManager.GetNewStoresAssets (App.instance.userObj);
				this.PrepareStoreLogo (App.instance.userObj.stores, App.instance.storeManager);
				storeListView.ItemsSource = App.instance.userObj.stores;
			} else {
				await DisplayAlert ("No Internet", "Please connect to the Internet to add new store.", "OK");
			}
			loadingMask.IsRunning = false;
		}

		private void PrepareStoreLogo(List<StoreObj> stores, StoreManager storeManager){
			foreach (StoreObj store in stores) {
				store.uiXsLogoImagePath = storeManager.xsLogoDirectoryPath + store._id + ".png";
			}
		}
	}
}

