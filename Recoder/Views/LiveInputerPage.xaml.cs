using System;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;

using Windows.UI.Xaml.Controls;
using Recoder.Core.Services;
using Recoder.Core.Models;
using Recoder.Helpers;

namespace Recoder.Views
{
    public sealed partial class LiveInputerPage : Page, INotifyPropertyChanged
    {
        private Match match = new Match();
        private MatchHelper helper = new MatchHelper();
        private List<Tag> tags = new List<Tag>();
        private BasicTag baseTag = new BasicTag();
        private int FaultCount = 0;

        public LiveInputerPage()
        {
            InitializeComponent();
            Match match = new Match();
            BasicTag baseTag = new BasicTag();
            match.Setup_Tester();
            match.Init_Match();
        }

        private void Init_Serve() {
            tags.Clear();
            FaultCount = 0;
            FaultButton_text.Text = "フォールト";
        }

        private void AddPoint(string team, List<Tag> tags, int rally = 0) {
            match.Add_Point(team, tags, rally);
            Cnt.Text = helper.GenerateCountText(match.PointA, match.PointB);
            Init_Serve();
        }

        private void Fault(bool AddTag = true) {
            if (AddTag) {
                if(FaultCount == 0) {
                    tags.Add(baseTag.Fault);
                    // TextBlock tag = helper.tagText;
                    // tag.Text = "フォールト";
                    // Commandbar_Content.Children.Add(tag);
                    FaultCount++;
                }
                else if(FaultCount == 1) {
                    tags.RemoveAt(0);
                    tags.Add(baseTag.WFault);                    
                    AddPoint(match.ReServer, tags);
                    //Commandbar_Content.Children.Remove(Commandbar_Content.Children.Last());
                    //TextBlock tag = helper.tagText;
                    //tag.Text = "ダブルフォールト";
                    //Commandbar_Content.Children.Add(tag);
                    //IsFault.IsEnabled = false;
                    //await Task.Delay(1500);
                    //Commandbar_Content.Children.Remove(Commandbar_Content.Children.Last());
                    // IsFault.IsEnabled = true;
                    FaultCount = 0;
                }
            }
            else {
                if (FaultCount == 0) {
                    FaultCount++;
                }
                else if (FaultCount == 1) {
                    tags.Add(baseTag.WFault);
                    AddPoint(match.ReServer, tags);
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
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

        private void IsAce_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            tags.Add(baseTag.ServiceAce);
            AddPoint(match.Server, tags);
        }

        private void IsFault_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            if (FaultCount == 0) {
                Fault();
                FaultButton_text.Text = "Wフォールト";
            }
            else if (FaultCount == 1) {
                Fault();
                Init_Serve();
            }
        }

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
    }
}
