// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace DWX.Bindings.IosApp
{
    [Register ("KittenDetailViewController")]
    partial class KittenDetailViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgKittenDetail { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCutenessDescription { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgKittenDetail != null) {
                imgKittenDetail.Dispose ();
                imgKittenDetail = null;
            }

            if (lblCutenessDescription != null) {
                lblCutenessDescription.Dispose ();
                lblCutenessDescription = null;
            }
        }
    }
}