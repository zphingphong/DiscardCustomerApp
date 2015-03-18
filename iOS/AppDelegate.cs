using Foundation;
using UIKit;

namespace com.panik.discard.ios {
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {
		public override bool FinishedLaunching (UIApplication app, NSDictionary options) {
			UIColor orangeColor = UIColor.FromRGB (255, 149, 0);
			UINavigationBar.Appearance.TintColor = orangeColor;
			UIBarButtonItem.Appearance.SetTitleTextAttributes (new UITextAttributes () {
				TextColor = orangeColor
			}, UIControlState.Normal);
//			UIApplication.SharedApplication.SetStatusBarStyle (UIStatusBarStyle.LightContent, animated: false);
			UILabel.Appearance.TextColor = orangeColor;
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (App.instance);

			return base.FinishedLaunching (app, options);
		}
	}
}

