﻿using System;
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
using Recoder.Controls;
using Recoder.Core.Models;
using Recoder.Helpers;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Recoder.Views.Inputer {
    public sealed partial class SetupPage3 : UserControl {

        private static SetupPage3_Core core;

        public string TeamNameA {
            get { return TeamNameA_TextBox.Text; }
            set { core.TeamNameA = value; }
        }

        public string A_Baseliner {
            get { return A_Back_TextBox.Text; }
            set { core.PlayerName_A_Baseliner = value; }
        }

        public string A_Volleyer {
            get { return A_Front_TextBox.Text; }
            set { core.PlayerName_A_Volleyer = value; }
        }

        public string TeamNameB {
            get { return TeamNameB_TextBox.Text; }
            set { core.TeamNameB = value; }
        }

        public string B_Baseliner {
            get { return B_Back_Textbox.Text; }
            set { core.PlayerName_B_Baseliner = value; }
        }

        public string B_Volleyer {
            get { return B_Front_Textbox.Text; }
            set { core.PlayerName_B_Volleyer = value; }
        }

        public SetupPage3_Core GetData() {
            var data = new SetupPage3_Core() {
                TeamNameA = TeamNameA,
                TeamNameB = TeamNameB,
                PlayerName_A_Baseliner = A_Baseliner,
                PlayerName_A_Volleyer = A_Volleyer,
                PlayerName_B_Baseliner = B_Baseliner,
                PlayerName_B_Volleyer = B_Volleyer
            };
            return data;
        }

        public SetupPage3() {
            this.InitializeComponent();
            if (core == null) {
                core = new SetupPage3_Core();
            }
        }

        private async void InputSubmitButton_Click(object sender, RoutedEventArgs e) {
            InputerPage.SetBaseData(GetData());
            MakeMatchInfo msg = new MakeMatchInfo();
            msg.SetFromMatchData(InputerPage.baseMatchData);
            await msg.ShowAsync();
            Services.NavigationService.Navigate(typeof(LiveInputerPage), InputerPage.baseMatchData);
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
