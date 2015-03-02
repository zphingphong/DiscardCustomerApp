using System;
using Xamarin.Auth;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace com.panik.discard {
	public class UserManager {
		private UserAccess userAccess;
		private UserService userService;
		public UserObj userObj;

		public string socialClientId;
		public string socialScope;
		public Uri socialAuthorizeUrl;
		public Uri socialRedirectUrl;

		public UserManager () {
			userAccess = new UserAccess ();
			userObj = new UserObj ();
			userService = new UserService ();
		}

		public void LoginUser (Account account, string userKey) {
			userObj.userKey = userKey;
			userObj.updateDateTimeObj = DateTime.Now;
			userObj.deviceId = DependencyService.Get<IDeviceManager> ().GetUniqueID ();
			// Make another call to get more user information
			switch (userObj.loginTypeEnum) {
				case UserObj.loginTypes.facebook:
					userService.GetUserInfoFromFacebook (userObj, account, ReceivedUserInfo);
					break;
				case UserObj.loginTypes.google:
					userService.GetUserInfoFromGoogle (userObj, account, ReceivedUserInfo);
					break;
			}
		}

		public async Task<UserObj> ReceivedUserInfo () {
			UserObj serverUser = await userService.LoginToServerAsync (userObj.ToJson());
			if (serverUser != null) { // Successfully login server
				if (userObj.id == null) { // Sign in for the first time from this device
					if (userObj.deviceId != serverUser.deviceId) { // This is not the same device that the user login last time
						// Ask the user if they are sure to login with this device, they will be logged out from the existing device once that device becomes online
						Device.BeginInvokeOnMainThread(async() => {
							var wantDeviceChanged = await ((LoginScreen)App.instance.MainPage).DisplayAlert ("", "Your account is associated with other device. We will clear all data from the other device, once you sign in with this device. Are you sure you would like to sign in?", "Yes", "No");

							// The user confirmed
							if (wantDeviceChanged) {
								// Sign the user in
								serverUser.deviceToClear = serverUser.deviceId; // Set deviceToClear to the existing device ID
								serverUser.deviceId = userObj.deviceId; // Set the device ID to the new device ID
								userObj = serverUser; // Replace local user with the user downloaded from the server with the new ID
								userAccess.CreateUser (userObj);
								// Set the new user with user.deviceToClear on the server
								userService.ChangeDeviceAsync (userObj.ToJson());
							} // The user declines, do nothing
						});
					} else {
						userObj = serverUser; // Replace local user with the user downloaded from the server
						userAccess.CreateUser (serverUser);
					}
				} else {
					//TODO: we may have to do something else later, in case that the datetime is not the same, but we overwite local, for now
					if(!userObj.updateDateTimeObj.Equals(serverUser.updateDateTimeObj)){ // Update time is different, overwrite local user object
						userObj = serverUser; // Replace local user with the user downloaded from the server
						userAccess.CreateUser (serverUser);
					}
				}
			}
			return userObj;
		}

		public void LoadExistingUser() {
			userObj = userAccess.RetrieveUserAsObj ();
		}
	}
}

