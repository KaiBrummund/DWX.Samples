using Foundation;
using System;
using UIKit;
using DWX.Bindings.Portable;
using System.Net;

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
        }

        public void UpdateCell(Kitten kitten)
        {
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += (s, e) =>
            {
                var bytes = e.Result;
                imgKittenPreview.Image = UIImage.LoadFromData(NSData.FromArray(bytes));
            };
            webClient.DownloadDataAsync(new Uri(kitten.ImageUrl));
            lblCutenesDescription.Text = kitten.CutenessDescription;
        }
    }
}