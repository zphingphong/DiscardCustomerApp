using System;
using System.IO;
using System.Net;
using System.Json;
using System.Threading.Tasks;

namespace com.panik.discard {
	public class StoreService {
		public StoreService () {
		}

		public void GetStoreXsLogo(StoreObj storeObj, string path){
			if (!File.Exists (Path.Combine (path, storeObj._id + ".png"))) {
				using (WebClient wc = new WebClient ()) {
					wc.DownloadFile (App.IMG_SERVER_ENDPOINT + "images/store_logo/xs/" + storeObj._id + ".png", Path.Combine (path, storeObj._id + ".png"));
				}
			}
		}

		public void GetStoreBgImg(StoreObj storeObj, string path){
			if (!File.Exists (Path.Combine (path, storeObj._id + ".png"))) {
				using (WebClient wc = new WebClient ()) {
					wc.DownloadFile (App.IMG_SERVER_ENDPOINT + "images/store_logo/lblur/" + storeObj._id + ".png", Path.Combine (path, storeObj._id + ".png"));
				}
			}
		}

		public void GetStoreStampImg(StoreObj storeObj, string path){
			if (!File.Exists (Path.Combine (path, storeObj._id + ".png"))) {
				using (WebClient wc = new WebClient ()) {
					wc.DownloadFile (App.IMG_SERVER_ENDPOINT + "images/store_stamp/" + storeObj._id + ".png", Path.Combine (path, storeObj._id + ".png"));
					wc.DownloadFile (App.IMG_SERVER_ENDPOINT + "images/store_stamp/" + storeObj._id + "free.png", Path.Combine (path, storeObj._id + "free.png"));
				}
			}
		}

		public async Task<StoreObj> GetStoreFromServer(int storeId){
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
		}

		public async Task<UserObj> AddCustomerStore(string storeId, string userId){
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
		}
	}
}

