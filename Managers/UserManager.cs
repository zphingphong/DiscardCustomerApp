using System;

namespace com.panik.discard {
	public class UserManager {
		private UserAccess userAccess = new UserAccess();
		private UserObj userObj = UserObj.instance;
		public UserManager () {
		}

		public void LoginUser () {
			userAccess.CreateUser (userObj);
		}
	}
}

