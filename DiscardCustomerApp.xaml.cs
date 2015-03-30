using System;
using System.IO;
using System.Collections.Generic;

using Xamarin.Forms;

namespace com.panik.discard {
	public partial class App : Application {

		private static readonly App _instance = new App ();
		public UserManager userManager;
		public StoreManager storeManager;
		public static object fileLocker;
		public const string SERVER_ENDPOINT = "http://192.168.1.35:3000/";
		public const string IMG_SERVER_ENDPOINT = "http://192.168.1.35:3000/";
		public UserObj userObj;
		private bool localUserNeedUpdate = false;

		private App () {
			InitializeComponent ();
			userManager =  new UserManager();
			storeManager = new StoreManager ();
			fileLocker = new object ();
			// Show login page only if no local user exist
			if (File.Exists (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "UserDoc"))) {
				if (DependencyService.Get<IDeviceManager> ().IsDeviceOnline ()) {
					userObj = userManager.GetUpdatedUser ();
					localUserNeedUpdate = true;
					storeManager.GetNewStoresAssets (userObj);
				} else {
					userObj = userManager.GetExistingUser ();
				}
				MainPage = new NavigationPage(new StoreListScreen (userObj.stores, storeManager));
			} else {
				userObj = new UserObj ();
				userObj.stores = new List<StoreObj> ();
				userObj.cards = new List<CardObj> ();
				MainPage = new LoginScreen ();
			}
		}

		public static App instance {
			get {
				return _instance; 
			}
		}

		protected override void OnStart () {
//			userObj = userManager.GetExistingUser ();
//			MainPage = new NavigationPage(new StoreListScreen (userObj.stores, storeManager));
			// TODO: Have to update local user data, but cannot write file on app initialize. May need to check more condition.
			if (localUserNeedUpdate) {
				userManager.UpdateLocalUser (userObj);
			}
		}

		protected override void OnSleep () {
			// Handle when your app sleeps
		}

		protected override void OnResume () {
			// Handle when your app resumes
		}
	}
}

