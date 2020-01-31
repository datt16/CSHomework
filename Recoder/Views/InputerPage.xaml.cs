using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;

using Windows.UI.Xaml.Navigation;
using System.Linq;

using Recoder.Services;
using Recoder.Helpers;

namespace Recoder.Views
{
    public sealed partial class InputerPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        

        public InputerPage()
        {
            InitializeComponent();

        }

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

        //private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
        //    NavigationService.Frame = SetupFrame;
        //    NavigationService.Navigate<SetupPage.SetupRoot>();
        //}
    }
}
