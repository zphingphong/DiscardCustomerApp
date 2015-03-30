﻿using System;

namespace com.panik.discard {
	public class StoreManager {
		private StoreAccess storeAccess;
		private StoreService storeService;
		public string xsLogoDirectoryPath { get; set; }
		public string bgImgDirectoryPath { get; set; }
		public string stampImgDirectoryPath { get; set; }

		public StoreManager () {
			storeService = new StoreService ();
			storeAccess = new StoreAccess ();
			xsLogoDirectoryPath = storeAccess.GetXsLogoDirectoryPath ();
			bgImgDirectoryPath = storeAccess.GetBgImgDirectoryPath ();
			stampImgDirectoryPath = storeAccess.GetStampImgDirectoryPath ();
		}

		public void GetNewStoresAssets (UserObj userObj) {
			foreach (StoreObj storeObj in userObj.stores) {
				storeService.GetStoreXsLogo (storeObj, xsLogoDirectoryPath);
				storeService.GetStoreBgImg (storeObj, bgImgDirectoryPath);
				storeService.GetStoreStampImg (storeObj, stampImgDirectoryPath);
			}
		}

		public StoreObj FindNewStore (int storeId) {
			return storeService.GetStoreFromServer (storeId);
		}

		public bool AddNewStore (StoreObj storeObj) {
			UserObj userObj = storeService.AddCustomerStore (storeObj._id, App.instance.userObj.id);
//			App.instance.userObj.stores.Add (storeObj);
			App.instance.userObj = userObj;
			App.instance.userManager.UpdateLocalUser (App.instance.userObj);
			storeService.GetStoreXsLogo (storeObj, xsLogoDirectoryPath);
			storeService.GetStoreBgImg (storeObj, bgImgDirectoryPath);
			storeService.GetStoreStampImg (storeObj, stampImgDirectoryPath);
			return true; // TODO: add user error handler
		}
	}
}

