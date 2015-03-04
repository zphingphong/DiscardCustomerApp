using System;
using System.Net;
using System.Json;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace com.panik.discard {
	public class UserService {

		public UserService () {
		}

		public void GetUserInfoFromFacebook (UserObj userObj, Account account, Func<Task> cb) {
			OAuth2Request oauthRequest = new OAuth2Request ("GET", new Uri ("https://graph.facebook.com/me"), null, account);
			oauthRequest.GetResponseAsync ().ContinueWith (t => {
				if (t.IsFaulted)
					Console.WriteLine ("Error: " + t.Exception.InnerException.Message);
				else {
					string result = t.Result.GetResponseText ();
					JsonValue resultObj = JsonValue.Parse (result);
					userObj.email = (string)resultObj ["email"];
					userObj.name = (string)resultObj ["name"];
					cb ();
				}
			});
		}

		public void GetUserInfoFromGoogle (UserObj userObj, Account account, Func<Task> cb) {
			OAuth2Request oauthRequest = new OAuth2Request ("GET", new Uri ("https://www.googleapis.com/plus/v1/people/me"), null, account);
			oauthRequest.GetResponseAsync ().ContinueWith (t => {
				if (t.IsFaulted)
					Console.WriteLine ("Error: " + t.Exception.InnerException.Message);
				else {
					string result = t.Result.GetResponseText ();
					JsonValue resultObj = JsonValue.Parse (result);
					userObj.email = (string)resultObj ["emails"] [0] ["value"]; // Get first email
					userObj.name = (string)resultObj ["displayName"];
					cb ();
				}
			});
		}

		public async Task<UserObj> LoginToServerAsync (string userJson) {
			string result = "";
			using (WebClient wc = new WebClient ()) {
				wc.Headers [HttpRequestHeader.ContentType] = "application/json";
				result = await wc.UploadStringTaskAsync (App.SERVER_ENDPOINT + "user/signin", userJson);
			}
			JsonValue resultObj = JsonValue.Parse (result);
			UserObj resultUserObj = null;
			if ((bool)resultObj ["success"]) {
				resultUserObj = UserObj.ParseUserFromJson(((JsonObject)resultObj ["user"]).ToString());
				resultUserObj.id = (string)resultObj ["user"] ["_id"];
			} else { // Fail to connect to the database
			}
			return resultUserObj;
		}

		public async Task<bool> ChangeDeviceAsync (string userJson) {
			string result = "";
			using (WebClient wc = new WebClient ()) {
				wc.Headers [HttpRequestHeader.ContentType] = "application/json";
				result = await wc.UploadStringTaskAsync (App.SERVER_ENDPOINT + "user/changedevice", userJson);
			}
			JsonValue resultObj = JsonValue.Parse (result);
			if ((bool)resultObj ["success"]) {
				return true;
			} else {
				return false;
			}
		}

		public UserObj GetUserFromServer (string userId){
			string result = "";
			using (WebClient wc = new WebClient ()) {
				result = wc.DownloadString (App.SERVER_ENDPOINT + "user/" + userId);
			}
			JsonValue resultObj = JsonValue.Parse (result);
			UserObj resultUserObj = null;
			if ((bool)resultObj ["success"]) {
				resultUserObj = UserObj.ParseUserFromJson(((JsonObject)resultObj ["user"]).ToString());
				resultUserObj.id = (string)resultObj ["user"] ["_id"];
			} else { // Fail to connect to the database
			}
			return resultUserObj;
		}
	}
}

