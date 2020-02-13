using System;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

using Windows.UI.Xaml;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

using Recoder.Controls.Result;
using Recoder.Core.Services;
using Recoder.Core.Models;
using Recoder.Helpers;
using Recoder.Controls;

namespace Recoder.Views
{
    using static Control_Alias;
    public sealed partial class LiveInputerPage : Page, INotifyPropertyChanged
    {
        private static Match match = new Match();
        private static MatchHelper helper = new MatchHelper();
        private static List<Tag> tags = new List<Tag>();
        private static BasicTag baseTag = new BasicTag();
        private static int FaultCount = 0, RallyCount = 0;
        private string server;
        public event PropertyChangedEventHandler PropertyChanged;
        public MatchData GameObject;

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            GameObject = e.Parameter as MatchData;
        }

        public LiveInputerPage()
        {
            InitializeComponent();
            BasicTag baseTag = new BasicTag();
            InitAllComponents();
        }

        private void InitAllComponents() {
            PointControl PtControl = new PointControl();
            Cnt.Text = helper.GenerateCountText(0, 0);
            GCnt.Text = helper.GenerateCountText(0, 0);
            RallyCountText.Text = "0";
            RallyCountDown.IsEnabled = false;
            Undo_Button.IsEnabled = false;
            CountResult.InitRoot(match.data.TeamAName, match.data.TeamBName);
            CountResult.ClearAll();
            MainGrid.Children.Clear();
        }

        private void InitCards() {
            // A_後衛
            MainGrid.Children.Add(MatchHelper.Set_PlayerCard(match.data.TeamAPlayers[0], match.data.TeamAName, SERVE   , SIDE_LEFT , POS_BOTTOM, POS_BACK , TEAM_A_BASELINER));
            // A_前衛
            MainGrid.Children.Add(MatchHelper.Set_PlayerCard(match.data.TeamAPlayers[1], match.data.TeamAName, NONE    , SIDE_LEFT , POS_TOP   , POS_FRONT, TEAM_A_VOLLEYER));
            // B_後衛
            MainGrid.Children.Add(MatchHelper.Set_PlayerCard(match.data.TeamBPlayers[0], match.data.TeamBName, RE_SERVE, SIDE_RIGHT, POS_TOP   , POS_BACK , TEAM_B_BASELINER));
            // B_前衛
            MainGrid.Children.Add(MatchHelper.Set_PlayerCard(match.data.TeamBPlayers[1], match.data.TeamBName, NONE    , SIDE_RIGHT, POS_BOTTOM, POS_FRONT, TEAM_B_VOLLEYER));
        }

        private async void SelectServer() {
            ServiceSelector serviceSelector = new ServiceSelector();
            serviceSelector.setDialog(match.data);
            serviceSelector.SetData += new EventHandler(GetServer);
            await serviceSelector.ShowAsync();
        }

        private void GetServer(object sender, System.EventArgs e) {
            server = MatchHelper.GetDialogData(sender).ToString();
            Match.Server = server;
            if (server == TEAM_A) {
                Match.ReServer = TEAM_B;
            }
            else if (server == TEAM_B) {
                Match.ReServer = TEAM_A;
            }
            MatchHelper.Update_Cards_IsServe(Match.Server, match.Point_index);
        }

        private void Init_Before_Serve() {
            tags.Clear();
            FaultCount = 0;
            RallyCount = 0;
            FaultButton_text.Text = "フォールト";
            RallyCountText.Text = "0";
            RallyCountDown.IsEnabled = false;
            MatchHelper.Update_Cards_IsServe(Match.Server, match.Point_index);
        }
        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        [Obsolete]
        private async void AddPoint(string team, List<Tag> tags) {
            Undo_Button.IsEnabled = true;
            string flag = match.Add_Point(team, tags, RallyCount);
            if (flag == "EndGame") {
                Undo_Button.IsEnabled = false;
            }
            else if (flag == "MatchEnd") {
                AllButton_To_NotEnabled();
                Cnt.Text = "試合終了";
                DataIO.JsonOutput(match.data);
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                await SettingsStorageExtensions.SaveAsync<MatchData>(storageFolder, "test", match.data);
                // Services.NavigationService.Navigate(typeof(MainDetailPage), null);
            }
            int cnt = 0;
            if (team == "A") cnt = match.PointA;
            else if (team == "B") cnt = match.PointB;
            CountResult.AddCount(new Point() { Getter = team, Tags = tags }, cnt);
            if (!Match.IsFinal) {
                Cnt.Text = helper.GenerateCountText(match.PointA, match.PointB);
                GCnt.Text = helper.GenerateCountText(match.GCountA, match.GCountB);
            }
            else if(Match.IsFinal) {
                Cnt.Text = helper.GenerateCountText(match.PointA, match.PointB, TYPE_ST_FINAL);
                GCnt.Text = helper.GenerateCountText(match.GCountA, match.GCountB, TYPE_ST_FINAL);
            }
            Init_Before_Serve();
        }

        [Obsolete]
        private void Fault(bool AddTag = true) {
            if (AddTag) {
                if(FaultCount == 0) {
                    tags.Add(baseTag.Fault);
                    FaultCount++;
                    MatchHelper.Cards[MatchHelper.ServerKey].Fault();
                }
                else if(FaultCount == 1) {
                    tags.RemoveAt(0);
                    tags.Add(baseTag.WFault);                    
                    AddPoint(Match.ReServer, tags);
                    FaultCount = 0;
                }
            }
            else {
                if (FaultCount == 0) {
                    FaultCount++;
                }
                else if (FaultCount == 1) {
                    tags.Add(baseTag.WFault);
                    AddPoint(Match.ReServer, tags);
                }
            }
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void Submit_to_A_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            TagCheck();
            helper.Debug_ShowTag(tags);
            AddPoint("A", tags);
            Record.Hide();
        }

        private void Submit_to_B_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            TagCheck();
            helper.Debug_ShowTag(tags);
            AddPoint("B", tags);
            Record.Hide();
        }

        [Obsolete]
        private void IsAce_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            tags.Add(baseTag.ServiceAce);
            AddPoint(Match.Server, tags);
        }

        [Obsolete]
        private void IsFault_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            if (FaultCount == 0) {
                Fault();
                FaultButton_text.Text = "Wフォールト";
            }
            else if (FaultCount == 1) {
                Fault();
                Init_Before_Serve();
            }
        }

        [Obsolete]
        private void IsFFault_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            tags.Add(baseTag.FootFault);
            if (FaultCount == 0) {
                Fault(false);
                FaultButton_text.Text = "Wフォールト";
                AddAnything_Flyout.Hide();
            }
            else if (FaultCount == 1) {
                Fault(false);
                FaultButton_text.Text = "フォールト";
                AddAnything_Flyout.Hide();
            }
        }

        private void IsServiceNet_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            tags.Add(baseTag.ServiceNet);
            if (FaultCount == 0) {
                Fault(false);
                FaultButton_text.Text = "Wフォールト";
                AddAnything_Flyout.Hide();
            }
            else if (FaultCount == 1) {
                Fault(false);
                FaultButton_text.Text = "フォールト";
                AddAnything_Flyout.Hide();
            }
        }

        private void TagCheck() {
            if (IsAce.IsChecked == true) tags.Add(baseTag.Ace);
            if (IsOut.IsChecked == true) tags.Add(baseTag.Out);
            if (IsTwoBounds.IsChecked == true) tags.Add(baseTag.TwoBounds);
            if (IsNet_on_Rally.IsChecked == true) tags.Add(baseTag.Net);
            IsAce.IsChecked = false;
            IsOut.IsChecked = false;
            IsTwoBounds.IsChecked = false;
            IsNet_on_Rally.IsChecked = false;
        }

        private void RallyCountDown_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            RallyCount--;
            RallyCountText.Text = RallyCount.ToString();
            if (RallyCount == 0) {
                RallyCountDown.IsEnabled = false;
            }
        }

        [Obsolete]
        private void Undo_Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            match.Undo();
            CountResult.ClearAll();
            CountResult.SetCount(match.GameCache);
            Init_Before_Serve();
            Undo_Button.IsEnabled = false;
            Cnt.Text = helper.GenerateCountText(match.PointA, match.PointB);
        }

        private async void _Loaded() {
            if (GameObject != null) {
                // GameObjectをmatch.dataに代入する処理
                match.data = GameObject;
                match.GamesCount = GameObject.GamesCount;
                GameObject.Title = "無題";
                ShowInfo.Text = GameObject.Title;
                // match.SettingMatch(GameObject);
                var msg = new ContentDialog
                {
                    Title = "試合が作成されました",
                    Content
                    = $"{match.data.TeamAName.ToString()}" +
                    $"\n{match.data.TeamAPlayers[0].Name.ToString()}" +
                    $"\n{match.data.GamesCount}ゲームマッチ",
                    PrimaryButtonText = "OK"
                };
                SelectServer();
                InitCards();
                // await msg.ShowAsync();
            }
            else {
                var msg = new ContentDialog
                {
                    Title = "確認",
                    Content = "GameDataの値がnullです",
                    PrimaryButtonText = "OK"
                };
                await msg.ShowAsync();
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) {
            _Loaded();
        }

        private void RallyCountUp_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            RallyCount++;
            RallyCountText.Text = RallyCount.ToString();
            if (RallyCount > 0) {
                RallyCountDown.IsEnabled = true;
            }
        }

        private void AllButton_To_NotEnabled() {
            IsFault.IsEnabled = false;
            IsServiceAce.IsEnabled = false;
            MoreServiceOptions.IsEnabled = false;
            AddRecordButton.IsEnabled = false;
        }
        
    }
}
