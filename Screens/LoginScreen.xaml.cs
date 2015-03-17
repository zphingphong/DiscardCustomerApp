using System;
using Xamarin.Forms;

namespace com.panik.discard {
	public partial class LoginScreen : ContentPage {
		public LoginScreen () {
			InitializeComponent ();
		}

		async void LoginWithFacebook (object sender, EventArgs args) {
			App.instance.userManager.socialClientId = "1553451278264664";
			App.instance.userManager.socialScope = "email";
			App.instance.userManager.socialAuthorizeUrl = new Uri ("https://m.facebook.com/dialog/oauth/");
			App.instance.userManager.socialRedirectUrl = new Uri ("http://www.facebook.com/connect/login_success.html");
			App.instance.userObj.loginTypeEnum = UserObj.loginTypes.facebook;
			await Navigation.PushModalAsync (new SocialLoginScreen ());
		}

		async void LoginWithGoogle (object sender, EventArgs args) {
			App.instance.userManager.socialClientId = "47378534221-mvmbh9v2a9103iobt76aemqi4vc1r7pq.apps.googleusercontent.com";
			App.instance.userManager.socialScope = "email";
			App.instance.userManager.socialAuthorizeUrl = new Uri ("https://accounts.google.com/o/oauth2/auth");
			App.instance.userManager.socialRedirectUrl = new Uri ("https://www.google.com/oauth2callback");
			App.instance.userObj.loginTypeEnum = UserObj.loginTypes.google;
			await Navigation.PushModalAsync (new SocialLoginScreen ());
		}

		public void GoToStoreListPage(){
			Navigation.PushModalAsync (new NavigationPage(new StoreListScreen (App.instance.userObj.stores, App.instance.storeManager)));
		}
	}
}

