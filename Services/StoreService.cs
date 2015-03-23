using System;
using System.IO;
using System.Net;
using System.Json;

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

		public StoreObj GetStoreFromServer(int storeId){
			string result = "";
			using (WebClient wc = new WebClient ()) {
				result = wc.DownloadString (App.SERVER_ENDPOINT + "store/" + storeId);
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

		public bool AddCustomerStore(string storeId, string userId){
			string result = "";
			using (WebClient wc = new WebClient ()) {
				result = wc.DownloadString (App.SERVER_ENDPOINT + "store/" + storeId + "/" + userId);
			}
			JsonValue resultObj = JsonValue.Parse (result);
			return (bool)resultObj ["success"];
		}
	}
}

