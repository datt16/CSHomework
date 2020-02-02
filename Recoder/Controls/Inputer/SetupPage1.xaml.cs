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
using Recoder.Helpers;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Recoder.Views.Inputer {
    public sealed partial class SetupPage1 : UserControl {
        public SetupPage1() {
            this.InitializeComponent();
        }
        private void GoLiveButton_Click(object sender, RoutedEventArgs e) {
            var RootPivotWindow = InputerPage.Rootpivot;
            RootPivotWindow.SelectedIndex += 1;
        }
    }
}
