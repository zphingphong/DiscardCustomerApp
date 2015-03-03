using Foundation;
using UIKit;

namespace com.panik.discard.ios {
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {
		public override bool FinishedLaunching (UIApplication app, NSDictionary options) {
//			UINavigationBar.Appearance.BarTintColor = UIColor.White;
			UIBarButtonItem.Appearance.SetTitleTextAttributes (new UITextAttributes () {
				TextColor = UIColor.FromRGB (255, 149, 0)
			}, UIControlState.Normal);
//			UIApplication.SharedApplication.SetStatusBarStyle (UIStatusBarStyle.LightContent, animated: false);
			UILabel.Appearance.TextColor = UIColor.FromRGB (255, 149, 0);
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (App.instance);

			return base.FinishedLaunching (app, options);
		}
	}
}

