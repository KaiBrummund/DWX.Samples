using Foundation;
using System;
using UIKit;
using DWX.Bindings.Portable;
using System.Net;
using SDWebImage;

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

        public override void PrepareForReuse()
        {
            base.PrepareForReuse();
            imgKittenPreview.Image = null;
        }
    }
}