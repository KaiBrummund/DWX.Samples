using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using DWX.Bindings.Portable;
using DWX.Bindings.AndroidApp.Common;

namespace DWX.Bindings.AndroidApp
{
    [Activity(Label = "DWX.Bindings.AndroidApp", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private CutenessViewModel ViewModel { get; set; }

        #region Stored UI Elements

        private ProgressBar _progressBar;
        private Button _refreshButton;

        private KittenAdapater _adapter;

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Get/Create the viewmodel
            ViewModel = new CutenessViewModel();

            // Load the layout
            SetContentView(Resource.Layout.Main);

            // Get all controls
            _refreshButton = FindViewById<Button>(Resource.Id.RefreshButton);
            _progressBar = FindViewById<ProgressBar>(Resource.Id.KittenDownloadProgress);

            _refreshButton.Click += (o, e) => { var t = ViewModel.RefreshAsync(); };

            var listView = FindViewById<ListView>(Resource.Id.KittenListView);
            listView.Adapter = _adapter = new KittenAdapater(this);
        }

        protected override void OnResume()
        {
            base.OnResume();

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        protected override void OnPause()
        {
            base.OnPause();

            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(e.PropertyName))
            {
                _Update_All();
            }
            else
            {
                switch (e.PropertyName)
                {
                    case nameof(CutenessViewModel.IsDataLoading):
                        _Update_IsDataLoading();
                        break;
                    case nameof(CutenessViewModel.Kittens):
                        _Update_Kittens();
                        break;
                    default:
                        break;

                }
            }
        }

        #region PropertyChangedHandlers

        private void _Update_All()
        {
            _Update_IsDataLoading();
            _Update_Kittens();
        }

        private void _Update_IsDataLoading()
        {
            _progressBar.Visibility = ViewModel.IsDataLoading ? ViewStates.Visible : ViewStates.Gone;
            _refreshButton.Enabled = !ViewModel.IsDataLoading;
        }

        private void _Update_Kittens()
        {
            _adapter.Kittens = ViewModel.Kittens.ToList();
        }

        #endregion

        #region ListViewAdapter

        public class KittenAdapater : BaseAdapter<Kitten>
        {
            private readonly Activity _context;
            private List<Kitten> _kittens = new List<Kitten>();

            public KittenAdapater(Activity context)
            {
                _context = context;
            }

            public override Kitten this[int position]
            {
                get
                {
                    return _kittens[position];
                }
            }

            public override int Count
            {
                get
                {
                    return _kittens.Count;
                }
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                if (convertView == null)
                {
                    convertView = _context.LayoutInflater.Inflate(Resource.Layout.CutenessCellLayout, parent, false);
                }

                var item = this[position];

                var image = convertView.FindViewById<ImageView>(Resource.Id.KittenCellImage);
                var text = convertView.FindViewById<TextView>(Resource.Id.KittenCellText);

                text.Text = item.CutenessDescription;

                DrawableManager.Instance.FetchDrawableOnThread(item.ImageUrl, image);

                return convertView;
            }

            public List<Kitten> Kittens
            {
                get { return _kittens; }
                set
                {
                    if (_kittens != value)
                    {
                        _kittens = value;
                        NotifyDataSetChanged();
                    }
                }
            }
        }

        #endregion
    }
}

