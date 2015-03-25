using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class StoreCardScreen : ContentPage {
		public StoreCardScreen (StoreObj store) {
			InitializeComponent ();
			storeInfoContainer.BindingContext = store;
			this.Title = store.name;
			this.BackgroundColor = Color.FromHex ("#" + store.themeColor);
			storeBgImageContainer.Source = App.instance.storeManager.bgImgDirectoryPath + store._id + ".png";
			App.instance.userManager.BuildQrImageDirectoryPath ();
			userInfoContainer.BindingContext = App.instance.userObj;

			// Generate stamps grid
			int noOfRows = (int)Math.Ceiling (((double)9 / 4));
			int stampCount = 0;
			string stampIconPath;
			for (int row = 0; row < noOfRows; row++) {
				for (int col = 0; col < 4 && stampCount < 9; col++, stampCount++) {
					if (stampCount < 6) {
						stampIconPath = App.instance.storeManager.stampImgDirectoryPath + store._id + ".png";
					} else {
						stampIconPath = "";
					}
					stampCardGrid.Children.Add (new Button {
						BackgroundColor = Color.FromRgba(255, 255, 255, 150),
						BorderColor = Color.Black,
						BorderWidth = 2,
						BorderRadius = 10,
						Image = stampIconPath,
						IsEnabled = false,
						WidthRequest = 50,
						HeightRequest = 50,
						HorizontalOptions = LayoutOptions.Center
					}, col, row);
				}
			}
		}

		protected override void OnDisappearing () {
			base.OnDisappearing();
			this.Title = "";
			MessagingCenter.Send<StoreCardScreen> (this, "FinishStoreCard");
		}
	}
}

