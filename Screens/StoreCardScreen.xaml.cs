using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class StoreCardScreen : ContentPage {
		public StoreCardScreen (StoreObj store) {
			InitializeComponent ();
			storeInfoContainer.BindingContext = store;
			App.instance.userManager.BuildQrImageDirectoryPath ();
			userInfoContainer.BindingContext = App.instance.userObj;

			// Generate stamps grid
			int noOfRows = (int)Math.Ceiling (((double)9 / 4));
			int stampCount = 0;
			string stampText;
			for (int row = 0; row < noOfRows; row++) {
				for (int col = 0; col < 4 && stampCount < 9; col++, stampCount++) {
					if (stampCount < 6) {
						stampText = "STAMP";
					} else {
						stampText = "-----";
					}
					stampCardGrid.Children.Add (new Label {
						Text = stampText,
						HorizontalOptions = LayoutOptions.Center
					}, col, row);
				}
			}
		}
	}
}

