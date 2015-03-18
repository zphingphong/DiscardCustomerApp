using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class AddStoreScreen : ContentPage {
		
		public AddStoreScreen () {
			InitializeComponent ();
		}

		public async void addStore (object sender, EventArgs e) {
			await Navigation.PopAsync ();
		}

		public async void findStore (object sender, EventArgs e) {
			this.BindingContext = App.instance.userObj.stores [0];
		}
	}
}

