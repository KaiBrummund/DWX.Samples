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
    [Register ("CuteKittenCell")]
    partial class CuteKittenCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView imgKittenPreview { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel lblCutenesDescription { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (imgKittenPreview != null) {
                imgKittenPreview.Dispose ();
                imgKittenPreview = null;
            }

            if (lblCutenesDescription != null) {
                lblCutenesDescription.Dispose ();
                lblCutenesDescription = null;
            }
        }
    }
}