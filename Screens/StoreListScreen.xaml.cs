using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class StoreListScreen : ContentPage {
		public string xsLogoDirectoryPath = (new StoreAccess()).GetXsLogoDirectoryPath();
		public StoreListScreen (List<StoreObj> stores) {
			InitializeComponent ();
			storeListView.ItemsSource = stores;
		}
	}
}

