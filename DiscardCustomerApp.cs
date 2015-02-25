using System;

using Xamarin.Forms;

namespace com.panik.discard {
	public class App : Application {

		public static string socialClientId;
		public static string socialScope;
		public static Uri socialAuthorizeUrl;
		public static Uri socialRedirectUrl;

		public App () {
			// The root page of your application
			MainPage = new LoginScreen();
//			MainPage = new SocialLoginScreen();
		}

		protected override void OnStart () {
			// Handle when your app starts
		}

		protected override void OnSleep () {
			// Handle when your app sleeps
		}

		protected override void OnResume () {
			// Handle when your app resumes
		}
	}
}

