using Recoder.Core.Models;
using Recoder.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recoder.Services {
    class MatchDataManager {

        public MatchDataManager() {
            Set_Sample();
        }

        static Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public static ObservableCollection<MatchData> Matches { get; private set; } = new ObservableCollection<MatchData>();

        public static void AddMatch(MatchData data) {
            if (data != null) {
                Matches.Add(data);
            }
        }

        public static void Set_Sample() {
            Matches = new ObservableCollection<MatchData>();
            Matches.Add(new MatchData {
                    Title = "TestMatch1",
                    TeamAName = "TeamA",
                    TeamBName = "TeamB",
                    GamesCount = 5,
                    TeamA_GamePoint = 1,
                    TeamB_GamePoint = 3,});
            Matches.Add(new MatchData {
                    Title = "TestMatch2",
                    TeamAName = "Apple",
                    TeamBName = "Microsoft",
                    GamesCount = 7,
                    TeamA_GamePoint = 4,
                    TeamB_GamePoint = 3,});
        }

        public static async void All_Clear() {
            Matches.Clear();
            await DataSaveAsync();
        }

        public static async Task DataSaveAsync() {
            await SettingsStorageExtensions.SaveAsync(storageFolder, "MatchList", Matches);
        }

        public static async Task DataLoadAsync() {
            Matches = new ObservableCollection<MatchData>();
            Matches = await SettingsStorageExtensions.ReadAsync<ObservableCollection<MatchData>>(storageFolder, "MatchList");
        }
    }
}
