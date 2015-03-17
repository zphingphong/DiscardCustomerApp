using System;
using System.IO;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class App : Application {

		private static readonly App _instance = new App ();
		public UserManager userManager;
		public StoreManager storeManager;
		public static object fileLocker;
		public const string SERVER_ENDPOINT = "http://192.168.1.71:3000/";
		public const string IMG_SERVER_ENDPOINT = "http://192.168.1.71:3000/";
		public UserObj userObj;

		private App () {
			InitializeComponent ();
			userManager =  new UserManager();
			storeManager = new StoreManager ();
			fileLocker = new object ();
			// Show login page only if no local user exist
			if (File.Exists (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "UserDoc"))) {
//				if (DependencyService.Get<IDeviceManager> ().IsDeviceOnline ()) {
//					userObj = userManager.GetUpdatedUser ();
//					storeManager.GetNewStoresLogo (userObj);
//				} else {
					userObj = userManager.GetExistingUser ();
//				}
				MainPage = new NavigationPage(new StoreListScreen (userObj.stores, storeManager));
			} else {
				userObj = new UserObj ();
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

