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

// コンテンツ ダイアログの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234238 を参照してください

namespace Recoder.Controls {
    public sealed partial class MakeMatchInfo : ContentDialog {
        private static SetupPage3_Core core;

        public string TeamNameA {
            get { return core.TeamNameA; }
            set { core.TeamNameA = value; }
        }

        public string A_Baseliner {
            get { return core.PlayerName_A_Baseliner; }
            set { core.PlayerName_A_Baseliner = value; }
        }

        public string A_Volleyer {
            get { return core.PlayerName_A_Volleyer; }
            set { core.PlayerName_A_Volleyer = value; }
        }

        public string TeamNameB {
            get { return core.TeamNameB; }
            set { core.TeamNameB = value; }
        }

        public string B_Baseliner {
            get { return core.PlayerName_B_Baseliner; }
            set { core.PlayerName_B_Baseliner = value; }
        }

        public string B_Volleyer {
            get { return core.PlayerName_B_Volleyer; }
            set { core.PlayerName_B_Volleyer = value; }
        }

        public int MatchGameCount {
            get { return core.MatchGameCount; }
            set { core.MatchGameCount = value; }
        }

        public void SetFromMatchData(MatchData data) {
            TeamNameA = data.TeamAName;
            TeamNameB = data.TeamBName;
            A_Baseliner = data.TeamAPlayers[0].Name;
            B_Baseliner = data.TeamBPlayers[0].Name;
            A_Volleyer = data.TeamAPlayers[1].Name;
            B_Volleyer = data.TeamBPlayers[1].Name;
            MatchGameCount = data.GamesCount;
        }

        public MakeMatchInfo() {
            this.InitializeComponent();
            if (core == null) {
                core = new SetupPage3_Core();
            }
        }

    }

    public class SetupPage3_Core {
        public string TeamNameA { get; set; }

        public string PlayerName_A_Baseliner { get; set; }

        public string PlayerName_A_Volleyer { get; set; }

        public string TeamNameB { get; set; }

        public string PlayerName_B_Baseliner { get; set; }

        public string PlayerName_B_Volleyer { get; set; }

        public int MatchGameCount { get; set; }
    }
}
