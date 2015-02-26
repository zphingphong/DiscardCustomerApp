using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace com.panik.discard.ios {
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {
		public override bool FinishedLaunching (UIApplication app, NSDictionary options) {
			global::Xamarin.Forms.Forms.Init ();
			UINavigationBar.Appearance.BarTintColor = UIColor.White;

			LoadApplication (App.instance);

			return base.FinishedLaunching (app, options);
		}
	}
}

