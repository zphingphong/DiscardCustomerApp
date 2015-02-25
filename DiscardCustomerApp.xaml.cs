using System;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class App : Application {

		private static readonly App _instance = new App ();
		public static string socialClientId;
		public static string socialScope;
		public static Uri socialAuthorizeUrl;
		public static Uri socialRedirectUrl;
		public UserManager userManager =  new UserManager();

		private App () {
			InitializeComponent ();
			// The root page of your application
			MainPage = new LoginScreen ();
		}

		public static App instance {
			get {
				return _instance; 
			}
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

