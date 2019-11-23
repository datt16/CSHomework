using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

namespace Test.Views
{
    public sealed partial class BlankPage : Page, INotifyPropertyChanged
    {
        public BlankPage()
        {
            InitializeComponent();
        }

        int ScoreA = 0, ScoreB = 0;

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

        private void TextBlock_SelectionChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e) {

        }

        private void CountUpButton_B_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            ScoreB++;
            Count_B.Text = ScoreB.ToString();
        }

        private void CountUpButton_A_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e) {
            ScoreA--;
            if (ScoreA >= 0) Count_A.Text = ScoreA.ToString();
        }

        private void CountUpButton_B_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e) {
            ScoreB--;
            if (ScoreB >= 0) Count_B.Text = ScoreB.ToString();
        }

        int BlockCount = 0,RowCount = 0;
        private void Test_AddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            BlockCount++;
            TextBlock TexB = new TextBlock();
            TextBlock Dummy = new TextBlock();
            TexB.Text = BlockCount.ToString() + "番目";
            TexB.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            TexB.Padding = new Windows.UI.Xaml.Thickness(0, 10, 0, 10);
            Dummy.Text = " ";
            Dummy.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
            Dummy.Padding = new Windows.UI.Xaml.Thickness(0, 10, 0, 10);
            Grid.SetRow(TexB, RowCount);
            if (RowCount == 9) {
                RowCount = 0;
                TestGrid.Children.Clear();
                TestGrid.Children.Add(TexB);
            }
            else {
                TestGrid.Children.Add(TexB);
            }
            
            RowCount++;
        }

        private void CountUpButton_A_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e) {
            ScoreA++;
            Count_A.Text = ScoreA.ToString();
        }
    }
}
