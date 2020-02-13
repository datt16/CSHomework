using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Microsoft.Toolkit.Uwp.UI.Controls;

using Recoder.Core.Models;
using Recoder.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Recoder.Views
{
    public sealed partial class LibraryPage : Page, INotifyPropertyChanged
    {
        private MatchData _selected;

        public MatchData Selected {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<MatchData> matches { get; private set; } = new ObservableCollection<MatchData>();

        public LibraryPage()
        {
            InitializeComponent();
            Loaded += LibraryPage_Loaded;
        }

        private void LibraryPage_Loaded(object sender, RoutedEventArgs e)
        {
            matches.Clear();

            foreach (var item in MatchDataManager.Matches) {
                if (item != null) {
                    item.SetDescription();
                    matches.Add(item);
                }
            }

            if (MasterDetailsViewControl.ViewState == MasterDetailsViewState.Both)
            {
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
