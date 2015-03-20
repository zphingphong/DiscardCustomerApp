using System;

namespace com.panik.discard {
	public class StoreManager {
		private StoreAccess storeAccess;
		private StoreService storeService;
		public string xsLogoDirectoryPath { get; set; }

		public StoreManager () {
			storeService = new StoreService ();
			storeAccess = new StoreAccess ();
			xsLogoDirectoryPath = storeAccess.GetXsLogoDirectoryPath ();
		}

		public void GetNewStoresLogo (UserObj userObj) {
			foreach (StoreObj storeObj in userObj.stores) {
				storeService.GetStoreXsLogo (storeObj, xsLogoDirectoryPath);
			}
		}

		public StoreObj FindNewStore (int storeId) {
			return storeService.GetStoreFromServer (storeId);
		}
	}
}

