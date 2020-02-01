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

        public int GameCount
        {   
            get { return (int)GetValue(GameCountproperty); }
            set {
                SetValue(GameCountproperty, value);
                ChooseGameCount.SelectedItem = value;
            }
        }

        public Request GetRequests() {
            var _requests = new Request() { MatchGameCount = GameCount };
            return _requests;
        }

        List<string> SelectCount = new List<string>() { "3","5","7" };

        public static readonly DependencyProperty GameCountproperty = DependencyProperty.Register(
            "GameCount",
            typeof(string),
            typeof(SetupPage2),
            new PropertyMetadata(0)
            );

        public SetupPage2() {
            this.InitializeComponent();
            
        }

        private async void GoNextWindowButton_Click(object sender, RoutedEventArgs e) {
            RootPivotWindow.SelectedIndex += 1;

            var data = this.GetRequests();
            await new MessageDialog($"{data.MatchGameCount.ToString()}マッチ", "確認").ShowAsync();

        }

        private void _Loaded(object sender, RoutedEventArgs e) {
            ChooseGameCount.SelectedItem = GameCount.ToString();
            var PvItem = Parent as PivotItem;
            RootPivotWindow = PvItem.Parent as Pivot;
        }

        private async void _SelectionChanged(object sender, SelectionChangedEventArgs e) {
            await new MessageDialog($"{ChooseGameCount.SelectedValue.ToString()}が選択されました", "タイトル").ShowAsync();
        }
    }

    public class Request {

        public int MatchGameCount { get; set; }

        public bool IsTeamMatching { get; set; }

        public bool IsSingles { get; set; }

        public bool IsNotFinal { get; set; }

        public bool IsNotDuace { get; set; }
    }   
}
