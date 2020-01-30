using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using System.Diagnostics;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// ユーザー コントロールの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=234236 を参照してください

namespace Recoder.Controls.Result {
    
    public sealed partial class PointChip : UserControl {

        public PointChip() {
            this.InitializeComponent();
        }

        public PointChip SetData(DataSet data, int DisplayCount = 0) {
            PointChip chip = new PointChip();
            chip.Current.Text = DisplayCount.ToString();
            chip.Sub.Text = data.Howto;
            Debug.WriteLine($"Getter : {data.Getter}");
            if (data.Getter == "A") {
                Grid.SetRow(chip.CurrentPanel, 1);
                Grid.SetRow(chip.SubPanel, 2);
            }
            else {
                Grid.SetRow(chip.CurrentPanel, 2);
                Grid.SetRow(chip.SubPanel, 1);
            }
            return chip;
        }
    }
}
