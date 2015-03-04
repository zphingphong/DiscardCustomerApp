using System;

namespace com.panik.discard {
	public class StoreManager {
		private StoreAccess storeAccess;
		private StoreService storeService;

		public StoreManager () {
			storeService = new StoreService ();
			storeAccess = new StoreAccess ();
		}

		public void GetNewStoresLogo (UserObj userObj) {
			foreach (StoreObj storeObj in userObj.stores) {
				storeService.GetStoreXsLogo (storeObj, storeAccess.GetXsLogoDirectoryPath());
			}
		}
	}
}

