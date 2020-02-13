using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.UI.Controls;

using Recoder.Core.Models;
using Recoder.Core.Services;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

namespace Recoder.Views
{
    public sealed partial class LibraryPage : Page, INotifyPropertyChanged
    {
        //private SampleOrder _selected;

        //public SampleOrder Selected
        //{
        //    get { return _selected; }
        //    set { Set(ref _selected, value); }
        //}


        private MatchData _selected;

        public MatchData Selected {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }


        // public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

        public ObservableCollection<MatchData> matches { get; private set; } = new ObservableCollection<MatchData>();

        public LibraryPage()
        {
            InitializeComponent();
            Loaded += LibraryPage_Loaded;
        }

        private async void LibraryPage_Loaded(object sender, RoutedEventArgs e)
        {
            // SampleItems.Clear();
            matches.Clear();

            // var data = await SampleDataService.GetMasterDetailDataAsync();
            matches.Add(
                new MatchData
                {
                    Title = "TestMatch!!",
                    TeamAName = "TeamA",
                    TeamBName = "TeamB",
                    GamesCount = 5,
                    TeamA_GamePoint = 1,
                    TeamB_GamePoint = 3,
                });
            matches.Add(
                new MatchData
                {
                    Title = "Mac VS Surfece Series",
                    TeamAName = "Apple",
                    TeamBName = "Microsoft",
                    GamesCount = 7,
                    TeamA_GamePoint = 4,
                    TeamB_GamePoint = 3,
                });

            //foreach (var item in data)
            //{
            //    SampleItems.Add(item);
            //}

            if (MasterDetailsViewControl.ViewState == MasterDetailsViewState.Both)
            {
                // Selected = SampleItems.FirstOrDefault();
                Selected = matches.FirstOrDefault();
            }
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
