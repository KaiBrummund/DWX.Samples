using Foundation;
using System;
using UIKit;
using DWX.Bindings.Portable;
using System.Net;

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

            lblCutenessDescription.Text = Kitten.CutenessDescription;

            imgKittenDetail.ClipsToBounds = true;
            var webClient = new WebClient();
            webClient.DownloadDataCompleted += (s, e) =>
            {
                var bytes = e.Result;
                imgKittenDetail.Image = UIImage.LoadFromData(NSData.FromArray(bytes));
            };
            webClient.DownloadDataAsync(new Uri(Kitten.ImageUrl));
        }
    }
}