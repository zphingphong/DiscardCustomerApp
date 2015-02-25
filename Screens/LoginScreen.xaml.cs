using System;
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

		async void LoginWithGoogle (object sender, EventArgs args) {
			App.socialClientId = "47378534221-mvmbh9v2a9103iobt76aemqi4vc1r7pq.apps.googleusercontent.com";
			App.socialScope = "https://www.googleapis.com/auth/userinfo.email";
			App.socialAuthorizeUrl = new Uri ("https://accounts.google.com/o/oauth2/auth");
			App.socialRedirectUrl = new Uri ("https://www.google.com/oauth2callback");
			await Navigation.PushModalAsync (new SocialLoginScreen ());
		}
	}
}

