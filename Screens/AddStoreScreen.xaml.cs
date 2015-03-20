using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class AddStoreScreen : ContentPage {
		private StoreObj storeObj  { get; set; }
		private Label storeNotFoundErr = new Label {
			Text = "There is no store associated with that ID. Please make sure the ID is correct.",
			XAlign = TextAlignment.Center
		};

		public AddStoreScreen () {
			InitializeComponent ();
			storeObj = new StoreObj ();
			this.BindingContext = storeObj;
		}

		public async void addStore (object sender, EventArgs e) {
			await Navigation.PopAsync ();
		}

		public void findStore (object sender, EventArgs e) {
			storeObj = App.instance.storeManager.FindNewStore (storeObj.storeId);
			if (storeObj == null) {
				addScreenContainer.Children.Add (storeNotFoundErr);
				storeObj = new StoreObj ();
				this.BindingContext = storeObj;
				addBtn.IsEnabled = false;
			} else {
				addScreenContainer.Children.Remove (storeNotFoundErr);
				storeObj.uiXsLogoImagePath = App.IMG_SERVER_ENDPOINT + "images/store_logo/xs/" + storeObj._id + ".png";
				this.BindingContext = storeObj;
				addBtn.IsEnabled = true;
			}
		}
	}
}

