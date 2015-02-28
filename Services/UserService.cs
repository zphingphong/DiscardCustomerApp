using System;
using System.Json;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace com.panik.discard {
	public class UserService {
		public UserService () {
		}

		public void GetUserInfoFromFacebook (UserObj userObj, Account account, Func<bool> cb) {
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

		public void GetUserInfoFromGoogle (UserObj userObj, Account account, Func<bool> cb) {
			OAuth2Request oauthRequest = new OAuth2Request ("GET", new Uri ("https://www.googleapis.com/plus/v1/people/me"), null, account);
			oauthRequest.GetResponseAsync ().ContinueWith (t => {
				if (t.IsFaulted)
					Console.WriteLine ("Error: " + t.Exception.InnerException.Message);
				else {
					string result = t.Result.GetResponseText ();
					JsonValue resultObj = JsonValue.Parse (result);
					userObj.email = (string)resultObj ["emails"][0]["value"]; // Get first email
					userObj.name = (string)resultObj ["displayName"];
					cb ();
				}
			});
		}

		public async Task<string> LoginToServerAsync () {
			return "";
		}
	}
}

