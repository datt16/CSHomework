using Recoder.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Recoder.Helpers;
using System.Diagnostics;
using Recoder.Services;

namespace Recoder.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public ObservableCollection<MatchData> matches { get; } = new ObservableCollection<MatchData>();

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            foreach(var item in MatchDataManager.Matches) {
                item.SetDescription();
                matches.Add(item);
            }
        }

        public MainPage()
        {
            InitializeComponent();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
