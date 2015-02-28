using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace com.panik.discard {
	public class UserAccess {
		private string userFilePath;
		private FileStream userFs;
		private DataContractJsonSerializer userJsonSerializer;

		public UserAccess () {
			userFilePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "UserDoc");
			userFs = new FileStream (userFilePath, FileMode.Create);
			userJsonSerializer = new DataContractJsonSerializer (typeof(UserObj));
		}

		public void CreateUser (UserObj userObj) {
			userObj.updateDateTimeObj = DateTime.Now;
			lock (App.fileLocker) {
				userJsonSerializer.WriteObject (userFs, userObj);
			}
		}

		public UserObj RetrieveUserAsObj () {
			lock (App.fileLocker) {
				return (UserObj)userJsonSerializer.ReadObject (userFs);
			}
		}

		public string RetrieveUserAsStr () {
			lock (App.fileLocker) {
				userFs.Position = 0;
				using (StreamReader userSr = new StreamReader (userFs)) {
					return userSr.ReadToEnd ();
				}
			}
		}
	}
}

