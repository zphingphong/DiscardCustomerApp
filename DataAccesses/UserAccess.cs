using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace com.panik.discard {
	public class UserAccess {
		public UserAccess () {
		}

		public void CreateUser (UserObj userObj) {
			userObj.id = "John";
			userObj.userKey = "TESTKEY";
			userObj.updateDateTimeObj = DateTime.Now;
			FileStream fs = new FileStream (Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "UserDoc"), FileMode.Create);
			DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(UserObj));
			jsonSerializer.WriteObject (fs, userObj);
		}
	}
}

