using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Recoder.Views;
using System.Threading.Tasks;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Recoder.Views.Inputer {
    public sealed partial class SetupPage2 : UserControl {

        private Pivot RootPivotWindow;
        private static SetupPage2_Core core;
        private readonly List<string> SelectCount = new List<string>() { "3","5","7" };

        public int GameCount
        {   
            get { return core.MatchGameCount; }
            set { core.MatchGameCount = value; }
        }
        public SetupPage2() {
            InitializeComponent();
            core = new SetupPage2_Core() { MatchGameCount = 5 };
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            ChooseGameCount.SelectedItem = GameCount.ToString();
            RootPivotWindow = InputerPage.Rootpivot;
        }

        public SetupPage2_Core GetData() {
            var _requests = new SetupPage2_Core() { MatchGameCount = GameCount };
            return _requests;
        }

        private async void GoNextWindowButton_Click(object sender, RoutedEventArgs e) {
            RootPivotWindow.SelectedIndex += 1;
            await Task.Delay(40);
            InputerPage.SetBaseData(GetData());
        }

    }

    public class SetupPage2_Core {

        public int MatchGameCount { get; set; }

        public bool IsTeamMatching { get; set; }

        public bool IsSingles { get; set; }

        public bool IsNotFinal { get; set; }

        public bool IsNotDuace { get; set; }
    }   
}
