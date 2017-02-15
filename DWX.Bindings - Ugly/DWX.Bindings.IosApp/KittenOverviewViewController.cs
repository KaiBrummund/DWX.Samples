using Foundation;
using System;
using UIKit;
using DWX.Bindings.Portable;

namespace DWX.Bindings.IosApp
{
    public partial class KittenOverviewViewController : UITableViewController
    {
       
        private CutenessViewModel ViewModel { get; set; }

        public KittenOverviewViewController (IntPtr handle) : base (handle)
        {
            ViewModel = new CutenessViewModel();
        }

        public async override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            ViewModel.RefreshAsync();
            UpdateKittens();
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) => UpdateKittens();

        private void UpdateKittens() => TableView.ReloadData();

        public override nint RowsInSection(UITableView tableView, nint section) => ViewModel.Kittens.Count;

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = (CuteKittenCell)tableView.DequeueReusableCell(nameof(CuteKittenCell));
            cell.UpdateCell(ViewModel.Kittens[indexPath.Row]);
            return cell;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            var dest = segue.DestinationViewController as KittenDetailViewController;
            dest.Kitten = ViewModel.Kittens[TableView.IndexPathForSelectedRow.Row];
        }
    }
}