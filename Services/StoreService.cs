using System;
using System.IO;
using System.Net;

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
	}
}

