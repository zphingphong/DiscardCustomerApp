using System;
using System.IO;
using Newtonsoft.Json;

namespace com.panik.discard {
	public class UserAccess {
		private string userFilePath;
		private readonly string qrImageDirectoryPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "images/user_qr/");
//		private DataContractJsonSerializer userJsonSerializer;

		public UserAccess () {
			userFilePath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "UserDoc");
			(new FileInfo (qrImageDirectoryPath)).Directory.Create ();
//			DataContractJsonSerializerSettings userSerSettings = new DataContractJsonSerializerSettings();
//			userSerSettings.EmitTypeInformation = EmitTypeInformation.Never;
//			userJsonSerializer = new DataContractJsonSerializer (typeof(UserObj));
		}

		public void CreateUser (UserObj userObj) {
			userObj.updateDateTimeObj = DateTime.Now;
			lock (App.fileLocker) {
				string userJson = JsonConvert.SerializeObject (userObj);
				File.WriteAllText (userFilePath, userJson);
			}
		}

		public UserObj RetrieveUserAsObj () {
			lock (App.fileLocker) {
				string userJson = File.ReadAllText (userFilePath);
				return JsonConvert.DeserializeObject<UserObj>(userJson);
			}
		}

		public string RetrieveUserAsStr () {
			lock (App.fileLocker) {
				return File.ReadAllText (userFilePath);
			}
		}

		public void UpdateUser (UserObj userObj){
		}

		public string GetQrImageDirectoryPath(){
			return qrImageDirectoryPath;
		}
	}
}

