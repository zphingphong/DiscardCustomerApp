using System;
using Xamarin.Forms;

namespace com.panik.discard {
	public partial class LoginScreen : ContentPage {
		public LoginScreen () {
			InitializeComponent ();
		}

		async void LoginWithFacebook (object sender, EventArgs args) {
			if (DependencyService.Get<IDeviceManager> ().IsDeviceOnline ()) {
				loadingMask.IsRunning = true;
				App.instance.userManager.socialClientId = "1553451278264664";
				App.instance.userManager.socialScope = "email";
				App.instance.userManager.socialAuthorizeUrl = new Uri ("https://m.facebook.com/dialog/oauth/");
				App.instance.userManager.socialRedirectUrl = new Uri ("http://www.facebook.com/connect/login_success.html");
				App.instance.userObj.loginType = UserObj.loginTypes.facebook;
				await Navigation.PushModalAsync (new SocialLoginScreen ());
			} else {
				await DisplayAlert ("No Internet", "Please connect to the Internet to sign in.", "OK");
			}
		}

		async void LoginWithGoogle (object sender, EventArgs args) {
			if (DependencyService.Get<IDeviceManager> ().IsDeviceOnline ()) {
				loadingMask.IsRunning = true;
				App.instance.userManager.socialClientId = "47378534221-mvmbh9v2a9103iobt76aemqi4vc1r7pq.apps.googleusercontent.com";
				App.instance.userManager.socialScope = "email";
				App.instance.userManager.socialAuthorizeUrl = new Uri ("https://accounts.google.com/o/oauth2/auth");
				App.instance.userManager.socialRedirectUrl = new Uri ("https://www.google.com/oauth2callback");
				App.instance.userObj.loginType = UserObj.loginTypes.google;
				await Navigation.PushModalAsync (new SocialLoginScreen ());
			} else {
				await DisplayAlert ("No Internet", "Please connect to the Internet to sign in.", "OK");
			}
		}

		public void GoToStoreListPage(){
			loadingMask.IsRunning = false;
			Navigation.PushModalAsync (new NavigationPage(new StoreListScreen (App.instance.userObj.stores, App.instance.storeManager)));
		}

		public void CancelLogin(){
			loadingMask.IsRunning = false;
		}
	}
}

