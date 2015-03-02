using System;
using System.IO;
using System.Runtime.Serialization.Json;

namespace com.panik.discard {
	public class UserAccess {
		private string userFilePath;
		private DataContractJsonSerializer userJsonSerializer;

		public UserAccess () {
			userFilePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "UserDoc");
			userJsonSerializer = new DataContractJsonSerializer (typeof(UserObj));
		}

		public void CreateUser (UserObj userObj) {
			userObj.updateDateTimeObj = DateTime.Now;
			lock (App.fileLocker) {
				using (FileStream userFs = new FileStream (userFilePath, FileMode.Create)) {
					userJsonSerializer.WriteObject (userFs, userObj);
				}
			}
		}

		public UserObj RetrieveUserAsObj () {
			lock (App.fileLocker) {
				return (UserObj)userJsonSerializer.ReadObject (new FileStream (userFilePath, FileMode.Open));
			}
		}

		public string RetrieveUserAsStr () {
			lock (App.fileLocker) {
				using (StreamReader userSr = new StreamReader (new FileStream (userFilePath, FileMode.Open))) {
					return userSr.ReadToEnd ();
				}
			}
		}

		public void UpdateUser (UserObj userObj){
		}
	}
}

