using System;
using Xamarin.Auth;
using Xamarin.Forms.Platform.iOS;
using Xamarin.Forms;
using com.panik.discard;
using com.panik.discard.ios;

[assembly: ExportRenderer (typeof(SocialLoginScreen), typeof(SocialLoginRenderer))]

namespace com.panik.discard.ios {
	public class SocialLoginRenderer : PageRenderer {

		bool IsShown;

		public override void ViewDidAppear (bool animated) {
			base.ViewDidAppear (animated);

			// Fixed the issue that on iOS 8, the modal wouldn't be popped.
			// url : http://stackoverflow.com/questions/24105390/how-to-login-to-facebook-in-xamarin-forms
			if (!IsShown) {

				IsShown = true;

				var auth = new OAuth2Authenticator (
					clientId: App.socialClientId, // your OAuth2 client id
					scope: App.socialScope, // The scopes for the particular API you're accessing. The format for this will vary by API.
					authorizeUrl: App.socialAuthorizeUrl, // the auth URL for the service
					redirectUrl: App.socialRedirectUrl // the redirect URL for the service
				);

				auth.Completed += (sender, eventArgs) => {
					// We presented the UI, so it's up to us to dimiss it on iOS.
					// App.Instance.SuccessfulLoginAction.Invoke ();
					DismissViewController (true, null);
					App.instance.MainPage.Navigation.PopModalAsync();
					App.instance.userManager.LoginUser();

					if (eventArgs.IsAuthenticated) {
						// Use eventArgs.Account to do wonderful things
						string accessToken = eventArgs.Account.Properties ["access_token"];
					} else {
						// The user cancelled
					}
				};

				PresentViewController (auth.GetUI (), true, null);

			}

		}
	}
}
