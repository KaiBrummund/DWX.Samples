using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DWX.Bindings.Portable
{
    public class CutenessViewModel : INotifyPropertyChanged
    {
        private bool _isDataLoading;
        private ObservableCollection<Kitten> _kittens = new ObservableCollection<Kitten>();

        // Properties
        public bool IsDataLoading
        {
            get { return _isDataLoading; }
            set
            {
                if (_isDataLoading != value)
                {
                    _isDataLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Kitten> Kittens
        {
            get { return _kittens; }
            set
            {
                if (_kittens != value)
                {
                    _kittens = value;
                    OnPropertyChanged();
                }
            }
        }

        // Methods

        public async Task RefreshAsync()
        {
            if (IsDataLoading) return;
            IsDataLoading = true;

            try
            {
                var client = new HttpClient();
                var json = await client.GetStringAsync("http://kaas.azurewebsites.net/api/kittens");

                var items = JsonConvert.DeserializeObject<List<Kitten>>(json);

                Kittens = new ObservableCollection<Kitten>(items);
            }
            finally
            {
                IsDataLoading = false;
            }
        }

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
