using System;
using System.IO;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class App : Application {

		private static readonly App _instance = new App ();
		public UserManager userManager;
		public static object fileLocker;
		public const string SERVER_ENDPOINT = "http://192.168.1.71:3000/";

		private App () {
			InitializeComponent ();
			userManager =  new UserManager();
			fileLocker = new object ();
			// Show login page only if no local user exist
			if (File.Exists (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "UserDoc"))) {
				userManager.LoadExistingUser ();
			} else {
				MainPage = new LoginScreen ();
			}
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

