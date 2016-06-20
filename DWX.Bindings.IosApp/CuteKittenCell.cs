using Foundation;
using System;
using UIKit;
using SDWebImage;
using DWX.Bindings.Portable;

namespace DWX.Bindings.IosApp
{
    public partial class CuteKittenCell : UITableViewCell
    {
        public CuteKittenCell (IntPtr handle) : base (handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            imgKittenPreview.ClipsToBounds = true;
        }

        public void UpdateCell(Kitten kitten)
        {
            imgKittenPreview.SetImage(new NSUrl(kitten.ImageUrl), UIImage.FromBundle("kitten_placeholder.png"));
            lblCutenesDescription.Text = kitten.CutenessDescription;
        }
    }
}