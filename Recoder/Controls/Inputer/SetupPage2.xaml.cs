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

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Recoder.Views.Inputer {
    public sealed partial class SetupPage2 : UserControl {
        public SetupPage2() {
            this.InitializeComponent();
        }

        private void GoNextWindowButton_Click(object sender, RoutedEventArgs e) {
            var parentGrid = Parent as Grid;
            var PvItem = parentGrid.Parent as PivotItem;
            var RootPivotWindow = PvItem.Parent as Pivot;
            RootPivotWindow.SelectedIndex += 1;
        }
    }
}
