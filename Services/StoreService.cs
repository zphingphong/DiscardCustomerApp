using System;
using System.IO;
using System.Net;
using System.Json;
using System.Threading.Tasks;

namespace com.panik.discard {
	public class StoreService {
		public StoreService () {
		}

		public void GetStoreXsLogo (StoreObj storeObj, string path) {
			try {
				if (!File.Exists (Path.Combine (path, storeObj._id + ".png"))) {
					using (WebClient wc = new WebClient ()) {
						wc.DownloadFile (App.IMG_SERVER_ENDPOINT + "images/store_logo/xs/" + storeObj._id + ".png", Path.Combine (path, storeObj._id + ".png"));
					}
				}
			} catch (Exception e) {
				//TODO: Tell the user that there's an error
				Console.WriteLine (e.ToString ());
			}
		}

		public void GetStoreBgImg (StoreObj storeObj, string path) {
			try {
				if (!File.Exists (Path.Combine (path, storeObj._id + ".png"))) {
					using (WebClient wc = new WebClient ()) {
						wc.DownloadFile (App.IMG_SERVER_ENDPOINT + "images/store_logo/lblur/" + storeObj._id + ".png", Path.Combine (path, storeObj._id + ".png"));
					}
				}
			} catch (Exception e) {
				//TODO: Tell the user that there's an error
				Console.WriteLine (e.ToString ());
			}
		}

		public void GetStoreStampImg (StoreObj storeObj, string path) {
			try {
				if (!File.Exists (Path.Combine (path, storeObj._id + ".png"))) {
					using (WebClient wc = new WebClient ()) {
						wc.DownloadFile (App.IMG_SERVER_ENDPOINT + "images/store_stamp/" + storeObj._id + ".png", Path.Combine (path, storeObj._id + ".png"));
						wc.DownloadFile (App.IMG_SERVER_ENDPOINT + "images/store_stamp/" + storeObj._id + "free.png", Path.Combine (path, storeObj._id + "free.png"));
					}
				}
			} catch (Exception e) {
				//TODO: Tell the user that there's an error
				Console.WriteLine (e.ToString ());
			}
		}

		public async Task<StoreObj> GetStoreFromServer (int storeId) {
			try {
				string result = "";
				using (WebClient wc = new WebClient ()) {
					result = await wc.DownloadStringTaskAsync (App.SERVER_ENDPOINT + "store/" + storeId);
				}
				JsonValue resultObj = JsonValue.Parse (result);
				StoreObj resultStoreObj = null;
				if ((bool)resultObj ["success"]) {
					if (resultObj ["store"] != null) {
						resultStoreObj = StoreObj.ParseFromJson (((JsonObject)resultObj ["store"]).ToString ());
					}
				} else { // Fail to connect to the database
				}
				return resultStoreObj;
			} catch (Exception e) {
				//TODO: Tell the user that there's an error
				Console.WriteLine (e.ToString ());
				return null;
			}
		}

		public async Task<UserObj> AddCustomerStore (string storeId, string userId) {
			try {
				string result = "";
				using (WebClient wc = new WebClient ()) {
					result = await wc.DownloadStringTaskAsync (App.SERVER_ENDPOINT + "store/" + storeId + "/" + userId);
				}
				JsonValue resultObj = JsonValue.Parse (result);
				if ((bool)resultObj ["success"]) {
					UserObj userObj = UserObj.ParseFromJson (((JsonObject)resultObj ["user"]).ToString ());
					userObj.id = (string)resultObj ["user"] ["_id"];
					return userObj;
				} else {
					return null;
				}
			} catch (Exception e) {
				//TODO: Tell the user that there's an error
				Console.WriteLine (e.ToString ());
				return null;
			}
		}
	}
}

