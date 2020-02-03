using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Recoder.Core.Models;
using Recoder.Helpers;

// コンテンツ ダイアログの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Recoder.Controls {
    using static Helpers.Control_Alias;
    public sealed partial class ServiceSelector : ContentDialog {
        private static MatchData data;

        public string Selected { get; set; }

        public event EventHandler SetData;


        public string TeamAName {
            get { return data.TeamAName; }
            set { data.TeamAName = value; }
        }

        public string TeamBName {
            get { return data.TeamBName; }
            set { data.TeamBName = value; }
        }

        public ServiceSelector() {
            InitializeComponent();
            if (data == null) {
                data = new MatchData();
            }
        }

        public void setDialog(MatchData d) {
            TeamAName = d.TeamAName;
            TeamBName = d.TeamBName;
        }

        private void SetAButton_Click(object sender, RoutedEventArgs e) {
            Selected = TEAM_A;
            SetData(this, EventArgs.Empty);
            Hide();
        }

        private void SetBButton_Click(object sender, RoutedEventArgs e) {
            Selected = TEAM_B;
            SetData(this, EventArgs.Empty);
            Hide();
        }
    }
}
