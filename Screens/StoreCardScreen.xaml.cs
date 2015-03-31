using System;
using System.Linq;
using System.Globalization;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class StoreCardScreen : ContentPage {
		public StoreCardScreen (StoreObj store) {
			InitializeComponent ();

			// Generate and bind store info
			storeInfoContainer.BindingContext = store;
			this.Title = store.name;
			this.BackgroundColor = Color.FromHex ("#" + store.themeColor);
			storeBgImageContainer.Source = App.instance.storeManager.bgImgDirectoryPath + store._id + ".png";

			// Generate and bind user infor
			App.instance.userManager.BuildQrImageDirectoryPath ();
			userInfoContainer.BindingContext = App.instance.userObj;

			// Generate stamps grid
			//TODO: Currently support 1 card, will have to support multiple
			try {
				CardObj card = App.instance.userObj.cards.Single (c => c.cardTemplate.storeId.Equals (store._id));
				if (card.cardTemplate.type == CardTemplateObj.cardTypes.stampCard) {
					int noOfRows = (int)Math.Ceiling ((((double)card.cardTemplate.stampsToRedeem + 1) / 4));
					int stampCount = 0;
					int col = 0;
					int row = 0;
					string stampIconPath;
					for (row = 0; row < noOfRows; row++) {
						for (col = 0; col < 4 && stampCount < card.cardTemplate.stampsToRedeem; col++, stampCount++) {
							if (stampCount < card.stamp) {
								stampIconPath = App.instance.storeManager.stampImgDirectoryPath + store._id + ".png";
							} else {
								stampIconPath = "";
							}
							stampCardGrid.Children.Add (new Button {
								BackgroundColor = Color.FromRgba (255, 255, 255, 150),
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
					stampCardGrid.Children.Add (new Button {
						Image = App.instance.storeManager.stampImgDirectoryPath + store._id + "free.png",
						IsEnabled = false,
						WidthRequest = 52,
						HeightRequest = 52,
						HorizontalOptions = LayoutOptions.Center
					}, col, row-1);
				}
				if (card.expiryDate != null) {
					storeCardContainer.Children.Add (new Label {
						TextColor = Color.White,
						Text = "Expires " + DateTime.Parse (card.expiryDate, CultureInfo.InvariantCulture).ToString ("MMMM d, yyyy"),
						XAlign = TextAlignment.Center
					});
				}
			} catch (InvalidOperationException ioe) {
			}
		}

		protected override void OnDisappearing () {
			base.OnDisappearing ();
			this.Title = "";
			MessagingCenter.Send<StoreCardScreen> (this, "FinishStoreCard");
		}
	}
}

