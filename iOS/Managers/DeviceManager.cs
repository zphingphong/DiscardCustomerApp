using System;
using com.panik.discard;
using UIKit;

[assembly: Xamarin.Forms.Dependency (typeof (com.panik.discard.ios.DeviceManager))]
namespace com.panik.discard.ios {
	public class DeviceManager : IDeviceManager {
		public DeviceManager () {
		}

		public string GetUniqueID () {
			return UIDevice.CurrentDevice.IdentifierForVendor.AsString();
		}
	}
}

