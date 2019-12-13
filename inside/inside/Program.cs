using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inside {
    class Program {
        static void Main(string[] args) {
            Recoder Match = new Recoder();
            Match.Start();
            Match.GameRecord();
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
        private String Server = "";

        private void Add(String Team) {
            if(Team == "a" || Team == "A" || Team == TeamNameA) {
                RecordA.Add(1);
                RecordB.Add(0);
                PCountA++;
                Console.WriteLine("Debug: +1point for teamA");
            }
            else if (Team == "b" || Team == "B" || Team == TeamNameB) {
                RecordA.Add(0);
                RecordB.Add(1);
                PCountB++;
            }
            else {
                // ここは後で例外処理にする
                throw new ArgumentException("a,b以外の文字列が入力されました。");
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
        private void BaseInfoOutput(params object[] a) {

            try {
                if (a[1].ToString() == "-t") {
                    Console.WriteLine("--- {0} ---", a[0]);
                    Console.WriteLine("A:{0} B:{1}", TeamNameA, TeamNameB);
                }
            }
            catch {
                Console.WriteLine("--- " + a[0] + " ---");
            }
        }
        public void Start() {
            Init();
            BaseInfoOutput("自分の名前を設定してください");
            this.TeamNameA = Console.ReadLine();
            BaseInfoOutput("対戦相手の名前を設定してください");
            this.TeamNameB = Console.ReadLine();
            BaseInfoOutput("サーバーを指定してください","-t");
            this.Server = Console.ReadLine();
            while(true){
                if (Server == "A" || Server == "B" || Server == "a" || Server == "b") {
                    break;
                }
                else {
                    BaseInfoOutput("もう一度サーバーを指定してください", "-t");
                    this.Server = Console.ReadLine();
                }
            }
        }

        private void Show() {
            int A_count = RecordA.Count(n => n == 1);
            int B_count = RecordB.Count(n => n == 1);
            if (Server == "a" || Server == "a") {
                Console.WriteLine("{2} | {0} - {1} | {3}", A_count, B_count, TeamNameA, TeamNameB);
            }
            else {
                Console.WriteLine("{2} | {0} - {1} | {3}", B_count, A_count, TeamNameB, TeamNameA);
            }
        }

       public void GameRecord() {
            // 1-game matching
            Console.WriteLine();
            this.Show();
            while (true) {
                BaseInfoOutput("ポイントを取得したチームを指定してください", "-t");
                string Input = Console.ReadLine();
                try {
                    this.Add(Input);
                }
                catch {
                    Console.WriteLine("AかBでチームを指定してください");
                }
                Console.WriteLine();
                this.Show();
            }
            
            // 次のポイントへ

        }

    }
}
