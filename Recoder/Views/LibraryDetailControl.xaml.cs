using System;
using System.Collections.Generic;
using Recoder.Core.Models;
using Recoder.Core.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Recoder.Views
{
    public sealed partial class LibraryDetailControl : UserControl
    {
        public MatchData MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as MatchData; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public List<string> playerNameList = new List<string>()
        {
            "a",
            "b",
            "c",
            "d"
        };

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(LibraryDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public LibraryDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as LibraryDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
        BasicTag bst = new BasicTag();

        private void SetData() {
            if (MasterMenuItem.TeamAPlayers != null) {
                A_Player1_text.Text = MasterMenuItem.TeamAPlayers[0].Name;
                A_Player0_text.Text = MasterMenuItem.TeamAPlayers[1].Name;
            }

            if (MasterMenuItem.TeamBPlayers != null) {
                B_Player1_text.Text = MasterMenuItem.TeamBPlayers[0].Name;
                B_Player0_text.Text = MasterMenuItem.TeamBPlayers[1].Name;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e) {
            if (MasterMenuItem != null) {
                SetData();
            }
        }

        private void UserControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args) {
            if (MasterMenuItem != null) {
                SetData();

            }
        }
    }

    public class Details {
        public int FirstServiceAceRate { get; set; }

        public int WFaultRate { get; set; }
    }
}
