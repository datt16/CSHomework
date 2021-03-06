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
using Windows.UI;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Recoder.Controls {
    public sealed partial class PlayerCard : UserControl {
        public bool IsServe_on_card = false;
        public bool IsReServe_on_card= false;
        private string PlayerName;
        private string TeamName;
        public int BallCount;

        private readonly SolidColorBrush HighLight = new SolidColorBrush(ColorHelper.FromArgb(255, 30, 90, 255));
        private readonly SolidColorBrush BaseColor = new SolidColorBrush(ColorHelper.FromArgb(255, 25, 25, 26));
        private readonly SolidColorBrush ServerColor = new SolidColorBrush(ColorHelper.FromArgb(40, 30, 90, 255));
        private readonly SolidColorBrush ReServerColor = new SolidColorBrush(ColorHelper.FromArgb(40, 255, 45, 0));


        public PlayerCard() {
            BallCount = 0;
            InitializeComponent();
            All_Invisibled();
            Init(false, false);
        }
        public void Init(bool Serve, bool ReServe) {
            All_Invisibled();
            BallCount = 0;
            IsServe_on_card = Serve;
            IsReServe_on_card = ReServe;
            if (IsServe_on_card) { // サーブ時:サーブ用パネルを表示、1球目のハイライトを表示
                MainBackground.Background = ServerColor;
                PanelServe.Visibility = Visibility.Visible; PanelServeText.Visibility = Visibility.Visible;
                //BallCount = 2;
                //PanelBall.Visibility = Visibility.Visible;
                //Ball1.Stroke = HighLight; // HighLightクラス : DogerBlueの色(ちょっと濃い青)
            }
            else if (IsReServe_on_card) {
                MainBackground.Background = ReServerColor;
                PanelReServe.Visibility = Visibility.Visible; PanelReServeText.Visibility = Visibility.Visible;
            }
            else {
                MainBackground.Background = BaseColor;
                PanelServe.Visibility = Visibility.Collapsed; PanelServeText.Visibility = Visibility.Collapsed;
                PanelReServe.Visibility = Visibility.Collapsed; PanelReServeText.Visibility = Visibility.Collapsed;
            }
        }
        public void Fault() {
            Ball1.Stroke = BaseColor;
            Ball1.Opacity = 0.1;
            Ball2.Stroke = HighLight;
            BallCount = 1;
        }

        private void All_Invisibled() {
            PanelServe.Visibility = Visibility.Collapsed; PanelServeText.Visibility = Visibility.Collapsed;
            PanelReServe.Visibility = Visibility.Collapsed; PanelReServeText.Visibility = Visibility.Collapsed;
            PanelBall.Visibility = Visibility.Collapsed;
        }

        public void SetPlayerName(string NewPlayerName) { // 名前をセット
            PlayerName = NewPlayerName;
            PlayerNameText.Text = NewPlayerName;
        }

        public void SetTeamName(string NewTeamName) { // チーム名をセット
            TeamName = NewTeamName;
            TeamNameText.Text = NewTeamName;
        }
    }
}
