using System;
using Xamarin.Auth;
using Xamarin.Forms;

namespace com.panik.discard {
	public class UserManager {
		private UserAccess userAccess = new UserAccess ();
		private UserService userService = new UserService ();
		private UserObj userObj = UserObj.instance;

		public string socialClientId;
		public string socialScope;
		public Uri socialAuthorizeUrl;
		public Uri socialRedirectUrl;

		public UserManager () {
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

		public bool ReceivedUserInfo () {
			userAccess.CreateUser (userObj);
			userService.LoginToServerAsync (userAccess.RetrieveUserAsStr());
			return true;
		}

		public UserObj GetUserObj(){
			return userObj;
		}
	}
}

