using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inside {
    class Program {
        static void Main(string[] args) {

        }
    }
    class Recoder {
        public void Test(String a) {
            Console.WriteLine(a);
        }
        // 試合記録用のListを入れ子にする
        public List<int> RecordA = new List<int>();
        public List<int> RecordB = new List<int>();
        public List<char> MatchA = new List<char>();
        public List<char> MatchB = new List<char>();

        private int PCountA, PCountB, GIndex;
        private bool Deuce = false;
        private String TeamNameA = "";
        private String TeamNameB = "";

        private void Add(String Team) {
            if(Team == "a" || Team == "A" || Team == TeamNameA) {
                RecordA.Append(1);
                RecordB.Append(0);
                PCountA++;
            }
            else if (Team == "b" || Team == "B" || Team == TeamNameB) {
                RecordA.Append(0);
                RecordB.Append(1);
                PCountB++;
            }
            else {
                // ここは後で例外処理にする
                BaseInfoOutput("入力を確認してください");
            }
            if((PCountA == 4 || PCountB == 4) && !Deuce) {
                // マッチデータ入力処理
                // 初期化して次のゲームへ
            }
        }

        private void DataUpdate() {
            
        }

        public void Init() {
            this.TeamNameA = ""; this.TeamNameB = "";
            this.PCountA = 0; this.PCountB = 0;
    }
        public void BaseInfoOutput(String a) {
            Console.WriteLine("--- " + a + " ---");
        }
        public void Start() {
            Init();
            BaseInfoOutput("自分の名前を設定してください");
            this.TeamNameA = Console.ReadLine();
            BaseInfoOutput("対戦相手の名前を設定してください");
            this.TeamNameB = Console.ReadLine();
        }

    }
}
