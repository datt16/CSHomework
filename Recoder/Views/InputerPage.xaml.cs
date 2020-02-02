using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml.Controls;

using Windows.UI.Xaml.Navigation;
using System.Linq;
using Recoder.Core.Models;
using Recoder.Views.Inputer;
using Recoder.Core.Services;
using System.Collections.Generic;

namespace Recoder.Views
{
    public sealed partial class InputerPage : Page, INotifyPropertyChanged
    {
        public static MatchData baseMatchData;
        public static Pivot Rootpivot;

        public InputerPage()
        {
            InitializeComponent();
            baseMatchData = new MatchData();
            Rootpivot = rootPivot;
        }

        public static void SetBaseData(object Value) {
            if (Value.GetType() == typeof(SetupPage2_Core)) {
                SetupPage2_Core data = Value as SetupPage2_Core;
                baseMatchData.GamesCount = data.MatchGameCount;
            }
            
            if (Value.GetType() == typeof(SetupPage3_Core)) {
                SetupPage3_Core data = Value as SetupPage3_Core;
                baseMatchData.TeamAName = data.TeamNameA;
                baseMatchData.TeamBName = data.TeamNameB;
                baseMatchData.TeamAPlayers = new List<Player>
                {
                    new Player()
                    {
                        Name = data.PlayerName_A_Baseliner,
                        Pos = Player_Position.Baseliner
                    },
                    new Player()
                    {
                        Name = data.PlayerName_A_Volleyer,
                        Pos = Player_Position.Volleyer
                    }
                };
                baseMatchData.TeamBPlayers = new List<Player>
                {
                    new Player()
                    {
                        Name = data.PlayerName_B_Baseliner,
                        Pos = Player_Position.Baseliner
                    },
                    new Player()
                    {
                        Name = data.PlayerName_B_Volleyer,
                        Pos = Player_Position.Volleyer
                    }
                };
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
    }
}
