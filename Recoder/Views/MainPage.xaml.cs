using Recoder.Core.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Recoder.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private ObservableCollection<MatchData> _matches = new ObservableCollection<MatchData>();

        public ObservableCollection<MatchData> matches {
            get { return this._matches; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            matches.Add(
                new MatchData
                {
                    TeamAName = "TeamA",
                    TeamBName = "TeamB",
                    GamesCount = 5,
                    TeamA_GamePoint = 1,
                    TeamB_GamePoint = 3,
                });
            matches.Add(
                new MatchData
                {
                    TeamAName = "Apple",
                    TeamBName = "Microsoft",
                    GamesCount = 7,
                    TeamA_GamePoint = 4,
                    TeamB_GamePoint = 3,
                });
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
