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

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Recoder.Views.Inputer {
    public sealed partial class SetupPage2 : UserControl {

        private Pivot RootPivotWindow;
        private static SetupPage2_Core core;
        private readonly List<string> SelectCount = new List<string>() { "3","5","7" };

        public int GameCount
        {   
            get { return core.MatchGameCount; }
            set {
                core.MatchGameCount = value;
                ChooseGameCount.SelectedItem = value;
            }
        }
        public SetupPage2() {
            this.InitializeComponent();
            core = new SetupPage2_Core() { MatchGameCount = 5 };
        }

        public SetupPage2_Core GetRequests() {
            var _requests = new SetupPage2_Core() { MatchGameCount = GameCount };
            return _requests;
        }

        private async void GoNextWindowButton_Click(object sender, RoutedEventArgs e) {
            RootPivotWindow.SelectedIndex += 1;

            var data = this.GetRequests();
            await new MessageDialog($"{data.MatchGameCount.ToString()}マッチ", "確認").ShowAsync();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            ChooseGameCount.SelectedItem = GameCount.ToString();
            var PvItem = Parent as PivotItem;
            RootPivotWindow = PvItem.Parent as Pivot;
        }

        private async void _SelectionChanged(object sender, SelectionChangedEventArgs e) {
            await new MessageDialog($"{ChooseGameCount.SelectedValue.ToString()}が選択されました", "タイトル").ShowAsync();
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
