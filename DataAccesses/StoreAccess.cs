using System;
using System.IO;

namespace com.panik.discard {
	public class StoreAccess {
		private readonly string xsLogoDirectoryPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "images/store_logo/xs/");
		public StoreAccess () {
			(new FileInfo (xsLogoDirectoryPath)).Directory.Create ();
		}

		public string GetXsLogoDirectoryPath(){
			return xsLogoDirectoryPath;
		}
	}
}

