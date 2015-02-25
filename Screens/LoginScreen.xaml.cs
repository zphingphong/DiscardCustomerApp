using System;
using System.Collections.Generic;
using Xamarin.Auth;
using Xamarin.Forms;

namespace com.panik.discard {
	public partial class LoginScreen : ContentPage {
		public LoginScreen () {
			InitializeComponent ();
		}

		async void LoginWithFacebook (object sender, EventArgs args) {
			App.socialClientId = "1553451278264664";
			App.socialScope = "email";
			App.socialAuthorizeUrl = new Uri ("https://m.facebook.com/dialog/oauth/");
			App.socialRedirectUrl = new Uri ("http://www.facebook.com/connect/login_success.html");
			await Navigation.PushModalAsync (new SocialLoginScreen ());
		}
	}
}

