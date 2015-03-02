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
					userObj = serverUser; // Replace local user with the user downloaded from the server
					userAccess.CreateUser (serverUser);
				} else {
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

