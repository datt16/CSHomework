using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Recoder.Core.Services;
using Recoder.Core.Models;
using Recoder.Helpers;
using System.Threading.Tasks;
using System.Diagnostics;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Recoder.Controls.Result {
    public sealed partial class ResultRoot : UserControl {

        public ResultRoot() {
            this.InitializeComponent();
        }

        public void InitRoot(string TeamNameA = "A", string TeamNameB = "B") {
            if (TeamNameA != null) TeamAText.Text = TeamNameA;
            if (TeamNameB != null) TeamBText.Text = TeamNameB;
            AScore.Text = "0";
            BScore.Text = "0";
        }

        [Obsolete]
        public void AddCount(Core.Models.Point point, int count) {
            if (count == 0) { ClearAll(); InitRoot(); return; }
            PointControl ptControl = new PointControl();
            ptControl.SetChip(point, PointsPanel, count);

            HistoryViewer.UpdateLayout();
            // TODO: [CS0618] 書き方が旧型式
            HistoryViewer.ScrollToHorizontalOffset(HistoryViewer.ScrollableWidth);

            if (point.Getter == "A") AScore.Text = count.ToString();
            else if (point.Getter == "B") BScore.Text = count.ToString();
        }

        [Obsolete]
        public void SetCount(Core.Models.Game game) {
            PointControl ptControl = new PointControl();
            var c = ptControl.AddChip_From_Game(game, PointsPanel);
            HistoryViewer.UpdateLayout();
            // HistoryViewer.ChangeView(10, HistoryViewer.ScrollableWidth, 1);
            // TODO: [CS0618] 書き方が旧型式
            HistoryViewer.ScrollToHorizontalOffset(HistoryViewer.ScrollableWidth);
            AScore.Text = c.Item1.ToString();
            BScore.Text = c.Item2.ToString();
        }

        public void ClearAll() {
            PointsPanel.Children.Clear();
        }
    }

    public class PointControl {
        public (int,int) AddChip_From_Game(Game game, FrameworkElement e) {
            ControlHelper helper = new ControlHelper();
            var set = e as StackPanel;
            var data = helper.GetDataSets(game);
            int a = 1, b = 1;
            foreach(var Pt in data) {
                PointChip chip = new PointChip();
                if (Pt.Getter == "A") {
                    chip = chip.SetData(Pt, a);
                    a++;
                }
                else if(Pt.Getter == "B") {
                    chip = chip.SetData(Pt, b);
                    b++;
                }
                else {
                    chip = chip.SetData(Pt, b);
                }
                Thickness mg = new Thickness(5, 0, 5, 0);
                chip.Margin = mg;
                set.Children.Add(chip);
                if (Pt.IsSpecial) {
                    var pts = new PointChipSeparator
                    {
                        Margin = mg
                    };
                    set.Children.Add(pts);
                }
            }
            return (a, b);
        }

        public void SetChip(Core.Models.Point point, FrameworkElement e, int CountIndex = 0) {
            ControlHelper helper = new ControlHelper();
            var set = e as StackPanel;
            DataSet data = new DataSet()
            {
                Getter = point.Getter,
                Howto = helper.Tag_to_String(point.Tags)
            };
            PointChip chip = new PointChip();
            chip = chip.SetData(data, CountIndex);
            Thickness mg = new Thickness(5, 0, 5, 0);
            chip.Margin = mg;
            set.Children.Add(chip);
            if (point.IsSpecial) {
                var pts = new PointChipSeparator
                {
                    Margin = mg
                };
                set.Children.Add(pts);
            }
        }
    }

    public class ControlHelper {
        public List<DataSet> GetDataSets(Game game) {
            List<DataSet> sets = new List<DataSet>();
            foreach(var data in game.Points) {
                DataSet set = new DataSet()
                {
                    Getter = data.Getter,
                    Howto = Tag_to_String(data.Tags)
                };
                if (data.IsSpecial) {
                    set.IsSpecial = true;
                }
                sets.Add(set);
                Debug.WriteLine($"Tag:{set.Howto}, {set.IsSpecial}");
            }
            return sets;
        }

        public string Tag_to_String(ICollection<Tag> tags) {
            StringBuilder res = new StringBuilder();
            foreach(Tag tag in tags) res.Append(tag.DisplayName + " ");
            return res.ToString();
        }
        
    }

    public class DataSet {
        public string Getter { get; set; }

        public string Howto { get; set; }

        public bool IsSpecial { get; set; } = false;
    }

}
