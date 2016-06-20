using Foundation;
using System;
using UIKit;
using DWX.Bindings.Portable;
using SDWebImage;

namespace DWX.Bindings.IosApp
{
    public partial class KittenDetailViewController : UIViewController
    {
        public Kitten Kitten { get; set; }

        public KittenDetailViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            imgKittenDetail.ClipsToBounds = true;
            imgKittenDetail.SetImage(new NSUrl(Kitten.ImageUrl));
            lblCutenessDescription.Text = Kitten.CutenessDescription;
        }
    }
}