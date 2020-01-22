using System;

using Recoder.Core.Models;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Recoder.Views
{
    public sealed partial class LibraryDetailControl : UserControl
    {
        public SampleOrder MasterMenuItem
        {
            get { return GetValue(MasterMenuItemProperty) as SampleOrder; }
            set { SetValue(MasterMenuItemProperty, value); }
        }

        public static readonly DependencyProperty MasterMenuItemProperty = DependencyProperty.Register("MasterMenuItem", typeof(SampleOrder), typeof(LibraryDetailControl), new PropertyMetadata(null, OnMasterMenuItemPropertyChanged));

        public LibraryDetailControl()
        {
            InitializeComponent();
        }

        private static void OnMasterMenuItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as LibraryDetailControl;
            control.ForegroundElement.ChangeView(0, 0, 1);
        }
    }
}
