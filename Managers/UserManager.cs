using System;
using Xamarin.Auth;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace com.panik.discard {
	public class UserManager {
		private UserAccess userAccess;
		private UserService userService;

		public string socialClientId;
		public string socialScope;
		public Uri socialAuthorizeUrl;
		public Uri socialRedirectUrl;

		public UserManager () {
			userAccess = new UserAccess ();
			userService = new UserService ();
		}

		public void LoginUser (Account account, string userKey) {
			App.instance.userObj.userKey = userKey;
			App.instance.userObj.updateDateTimeObj = DateTime.Now;
			App.instance.userObj.deviceId = DependencyService.Get<IDeviceManager> ().GetUniqueID ();
			// Make another call to get more user information
			switch (App.instance.userObj.loginTypeEnum) {
				case UserObj.loginTypes.facebook:
					userService.GetUserInfoFromFacebook (App.instance.userObj, account, LoginServerUser);
					break;
				case UserObj.loginTypes.google:
					userService.GetUserInfoFromGoogle (App.instance.userObj, account, LoginServerUser);
					break;
			}
		}

		public async Task<UserObj> LoginServerUser () {
			UserObj serverUser = await userService.LoginToServerAsync (App.instance.userObj.ToJson ());
			if (serverUser != null) { // Successfully login server
				if (App.instance.userObj.id == null) { // Sign in for the first time from this device
					if (App.instance.userObj.deviceId != serverUser.deviceId) { // This is not the same device that the user login last time
						// Ask the user if they are sure to login with this device, they will be logged out from the existing device once that device becomes online
						Device.BeginInvokeOnMainThread (async() => {
							var wantDeviceChanged = await ((LoginScreen)App.instance.MainPage).DisplayAlert ("", "Your account is associated with other device. We will clear all data from the other device, once you sign in with this device. Are you sure you would like to sign in?", "Yes", "No");

							// The user confirmed
							if (wantDeviceChanged) {
								// Sign the user in
								serverUser.deviceToClear = serverUser.deviceId; // Set deviceToClear to the existing device ID
								serverUser.deviceId = App.instance.userObj.deviceId; // Set the device ID to the new device ID
								App.instance.userObj = serverUser; // Replace local user with the user downloaded from the server with the new ID
								userAccess.CreateUser (App.instance.userObj);
								// Set the new user with user.deviceToClear on the server
								userService.ChangeDeviceAsync (App.instance.userObj.ToJson ());
								((LoginScreen)App.instance.MainPage).GoToStoreListPage ();
							} // The user declines, do nothing
						});
					} else {
						App.instance.userObj = serverUser; // Replace local user with the user downloaded from the server
						userAccess.CreateUser (serverUser);
						Device.BeginInvokeOnMainThread (async() => {
							((LoginScreen)App.instance.MainPage).GoToStoreListPage ();
						});
					}
				} else {
					//TODO: we may have to do something else later, in case that the datetime is not the same, but we overwite local, for now
					if (!App.instance.userObj.updateDateTimeObj.Equals (serverUser.updateDateTimeObj)) { // Update time is different, overwrite local user object
						App.instance.userObj = serverUser; // Replace local user with the user downloaded from the server
						userAccess.CreateUser (serverUser);
					}
					Device.BeginInvokeOnMainThread (async() => {
						((LoginScreen)App.instance.MainPage).GoToStoreListPage ();
					});
				}
			}
			return App.instance.userObj;
		}

		public UserObj GetExistingUser () {
			return userAccess.RetrieveUserAsObj ();
		}

		public UserObj GetUpdatedUser(){
			UserObj userObj = this.GetExistingUser ();
			return userService.GetUserFromServer(userObj.id);
		}
	}
}

