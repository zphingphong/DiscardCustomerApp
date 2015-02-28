using System;
using com.panik.discard;
using Android.Provider;

[assembly: Xamarin.Forms.Dependency (typeof (com.panik.discard.droid.DeviceManager))]
namespace com.panik.discard.droid {
	public class DeviceManager : IDeviceManager {
		public DeviceManager () {
		}

		public string GetUniqueID () {
			return Settings.Secure.GetString (MainActivity.GetAppContext().ContentResolver, Settings.Secure.AndroidId);
		}
	}
}

