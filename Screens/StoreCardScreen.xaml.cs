using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class StoreCardScreen : ContentPage {
		public StoreCardScreen (StoreObj store) {
			InitializeComponent ();
			this.BindingContext = store;

			// Generate stamps grid
			int noOfRows = (int)Math.Ceiling ((double)(9 / 4));
			for (int row = 0; row < noOfRows; row++) {
				for (int col = 0; col < 4; col++) {
					stampCardGrid.Children.Add (new Label {
						Text = "Grid",
						HorizontalOptions = LayoutOptions.Center
					}, row, col);
				}
			}
		}
	}
}

