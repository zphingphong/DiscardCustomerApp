﻿using System;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Json;
using System.Threading.Tasks;
using Xamarin.Auth;

namespace com.panik.discard {
	public class UserService {

		public UserService () {
		}

		public void GetUserQr (UserObj userObj, string path) {
			try {
				if (!File.Exists (Path.Combine (path, userObj.id + ".png"))) {
					using (WebClient wc = new WebClient ()) {
						wc.DownloadFile (App.IMG_SERVER_ENDPOINT + "images/user_qr/" + userObj.id + ".png", Path.Combine (path, userObj.id + ".png"));
					}
				}
			} catch (Exception e) {
				//TODO: Tell the user that there's an error
				Console.WriteLine (e.ToString());
			}
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
			try {
				string result = "";
				using (WebClient wc = new WebClient ()) {
					wc.Headers [HttpRequestHeader.ContentType] = "application/json";
					result = await wc.UploadStringTaskAsync (App.SERVER_ENDPOINT + "user/signin", userJson);
				}
				JsonValue resultObj = JsonValue.Parse (result);
				UserObj resultUserObj = null;
				if ((bool)resultObj ["success"]) {
					resultUserObj = UserObj.ParseFromJson (((JsonObject)resultObj ["user"]).ToString ());
					resultUserObj.id = (string)resultObj ["user"] ["_id"];
				} else { // Fail to connect to the database
				}
				return resultUserObj;
			} catch (Exception e) {
				//TODO: Tell the user that there's an error
				Console.WriteLine (e.ToString());
				return null;
			}
		}

		public async Task<bool> ChangeDeviceAsync (string userJson) {
			try {
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
			} catch (Exception e) {
				//TODO: Tell the user that there's an error
				Console.WriteLine (e.ToString());
				return false;
			}
		}

		public UserObj GetUserFromServer (string userId) {
			try {
				string result = "";
				using (WebClient wc = new WebClient ()) {
					wc.Headers.Add("Cache-Control", "no-cache");
					wc.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
					result = wc.DownloadString (App.SERVER_ENDPOINT + "user/" + userId);
				}
				return ParseUserFronResult (result);
			} catch (Exception e) {
				//TODO: Tell the user that there's an error
				Console.WriteLine (e.ToString());
				return null;
			}
		}

		public async Task<UserObj> GetUserFromServerTaskAsync (string userId) {
			try {
				string result = "";
				using (WebClient wc = new WebClient ()) {
					wc.Headers.Add("Cache-Control", "no-cache");
					wc.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
					result = await wc.DownloadStringTaskAsync (App.SERVER_ENDPOINT + "user/" + userId);
				}
				return ParseUserFronResult (result);
			} catch (Exception e) {
				//TODO: Tell the user that there's an error
				//				await DisplayAlert ("Oops!", "Something went wrong on our server. Please try again later.", "OK");
				Console.WriteLine (e.ToString());
				return null;
			}
		}

		private UserObj ParseUserFronResult (string result) {
			JsonValue resultObj = JsonValue.Parse (result);
			UserObj resultUserObj = null;
			if ((bool)resultObj ["success"]) {
				resultUserObj = UserObj.ParseFromJson (((JsonObject)resultObj ["user"]).ToString ());
				resultUserObj.id = (string)resultObj ["user"] ["_id"];
			} else { // Fail to connect to the database
			}
			return resultUserObj;
		}
	}
}

