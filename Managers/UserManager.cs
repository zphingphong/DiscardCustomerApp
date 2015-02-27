using System;
using Xamarin.Auth;

namespace com.panik.discard {
	public class UserManager {
		private UserAccess userAccess = new UserAccess();
		private UserService userService = new UserService();
		private UserObj userObj = UserObj.instance;

		public string socialClientId;
		public string socialScope;
		public Uri socialAuthorizeUrl;
		public Uri socialRedirectUrl;
		public int loginType;

		public UserManager () {
		}

		public void LoginUser (Account account, string userKey) {
			userObj.userKey = userKey;
			userObj.loginType = loginType;
			userObj.updateDateTimeObj = DateTime.Now;
			// Make another call to get more user information
			if (this.loginType == 0) {
				userService.GetUserInfoFromFacebook (userObj, account, ReceivedUserInfo);
			}
		}

		public bool ReceivedUserInfo(){
			userAccess.CreateUser (userObj);
			return true;
		}
	}
}

