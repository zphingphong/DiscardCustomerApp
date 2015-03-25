using System;
using System.IO;

namespace com.panik.discard {
	public class StoreAccess {
		private readonly string xsLogoDirectoryPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "images/store_logo/xs/");
		private readonly string bgImgDirectoryPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "images/store_logo/lblur/");
		private readonly string stampImgDirectoryPath = Path.Combine (Environment.GetFolderPath (Environment.SpecialFolder.Personal), "images/store_stamp/");
		public StoreAccess () {
			(new FileInfo (xsLogoDirectoryPath)).Directory.Create ();
			(new FileInfo (bgImgDirectoryPath)).Directory.Create ();
			(new FileInfo (stampImgDirectoryPath)).Directory.Create ();
		}

		public string GetXsLogoDirectoryPath(){
			return xsLogoDirectoryPath;
		}

		public string GetBgImgDirectoryPath(){
			return bgImgDirectoryPath;
		}

		public string GetStampImgDirectoryPath(){
			return stampImgDirectoryPath;
		}
	}
}

