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
using Recoder.Views;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Recoder.Views.Inputer {
    public sealed partial class SetupPage3 : UserControl {

        private static SetupPage3_Core core;

        public SetupPage3() {
            this.InitializeComponent();
            if (core == null) {
                core = new SetupPage3_Core();
            }
        }

        private void InputSubmitButton_Click(object sender, RoutedEventArgs e) {
            var inputpage = new LiveInputerPage();
            Services.NavigationService.Navigate(typeof(LiveInputerPage));
        }
    }

    public class SetupPage3_Core {
        public string TeamNameA { get; set; }

        public string PlayerName_A_Baseliner { get; set; }

        public string PlayerName_A_Volleyer { get; set; }

        public string TeamNameB { get; set; }

        public string PlayerName_B_Baseliner { get; set; }

        public string PlayerName_B_Volleyer { get; set; }
    }
}
