using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Recoder.Core.Models;
using Recoder.Helpers;
using Recoder.Controls;
using Recoder.Core.Services;

namespace Recoder.Helpers {
    using static Control_Alias;
    using static Positon_Alias;
    public class MatchHelper {

        public static Dictionary<string, PlayerCard> Cards = new Dictionary<string, PlayerCard>();
        public static string ServerKey = "";
        public TextBlock tagText = new TextBlock()
        {
            Text = "",
            Margin = new Thickness(5, 10, 10, 10),
            FontSize = 12
        };

        /// <summary>
        /// カウントを表示するオブジェクト用のテキストを生成。
        /// <list type="bullet">
        /// <listheader>第三引数typeに指定できる文字列一覧</listheader>
        ///     <item>
        ///         <term>SoftTennis</term>
        ///         <description>ソフトテニス用のカウント表示。[デュースあり、3-3からデュース] ex: Adv-3</description>
        ///     </item>
        ///     <item>
        ///         <term>Base</term>
        ///         <description>汎用的なカウント表示。[デュースなし、0補完あり] ex: 11-09</description>
        ///     </item>
        ///     <item>
        ///         <term>SoftTennis_Final</term>
        ///         <description>ファイナルゲーム用。[デュースあり、6-6からデュース]</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="CountA">Aサイド側のポイント</param>
        /// <param name="CountB">Bサイド側のポイント</param>
        /// <param name="type">["Base","SoftTennis","SoftTennis_Final"]</param>
        /// <returns></returns>
        public string GenerateCountText(int CountA, int CountB, string type = "SoftTennis") {
            if (type == TYPE_BASIC) {
                if (CountA >= 10) {
                    return CountA.ToString() + " - 0" + CountB.ToString();
                }
                else if (CountB >= 10) {
                    return "0" + CountA.ToString() + " - " + CountB.ToString();
                }
            }
            else if (type == TYPE_ST) {
                if (CountA > CountB && CountA >= 4) {
                    return "Adv - " + CountB.ToString();
                }
                else if (CountB > CountA && CountB >= 4) {
                    return CountA.ToString() + " - Adv";
                }
            }
            else if (type == TYPE_ST_FINAL) {
                if (CountA > CountB && CountA >= 6) {
                    return "Adv - " + CountB.ToString();
                }
                else if (CountB > CountA && CountB >= 6) {
                    return CountA.ToString() + " - Adv";
                }
            }
            return CountA.ToString() + " - " + CountB.ToString();
        }

        private static void NullCheck(params object[] obj) {
            foreach(object o in obj) {
                if (o == null) Debug.WriteLine($"{o.ToString()} is Null.");
            }
        }

        public static object GetDialogData(object e) {
            if (e.GetType() == typeof(ServiceSelector)) {
                string ServerTeam;
                ServiceSelector Element = e as ServiceSelector;
                ServerTeam = Element.Selected;
                return ServerTeam;
            }
            return null;
        }

        /// <summary>
        /// プレイヤーカードのセットアップ
        /// </summary>
        public static PlayerCard Set_PlayerCard(Player player, string NewTeamName, string IsServe, string Side, string yPos, string xPos, string Key) {
            NullCheck(player, NewTeamName, IsServe, Side);
            PlayerCard card = new PlayerCard();
            card.InitializeComponent();

            string NewPlayerName = player.Name;

            Init_Card(card, NewTeamName, NewPlayerName, IsServe);
            Set_CardPos(card, yPos, Side, xPos);
            Cards.Add(Key, card);
            return card;
        }

        /// <summary>
        /// カードの初期化
        /// </summary>
        private static void Init_Card(PlayerCard Card, string TName, string PName, string pos) {
            bool Serve = false, ReServe = false;
            if (pos == SERVE) Serve = true;
            else if (pos == RE_SERVE) ReServe = true;
            Debug.WriteLine($"{TName}: {PName}, {pos}, Serve :{Serve}, Reserve : {ReServe}");
            Card.Init(Serve, ReServe);
            Card.SetPlayerName(PName);
            Card.SetTeamName(TName);
            Card.Width = 150;
            Card.Height = 220;
        }

        /// <summary>
        /// カードのポジション設定
        /// </summary>
        public static void Set_CardPos(PlayerCard Card, string IsTop, string IsLeft, string IsFront) {
            int row = 0;
            int col = 0;
            Thickness Margin = new Thickness(0, 0, 0, 0);
            Card_Positon tag = Left_Front_Top;
            if (IsLeft == SIDE_LEFT) {
                if(IsFront == POS_FRONT) {
                    if (IsTop == POS_TOP) {
                        tag = Left_Front_Top;
                    }
                    else {
                        tag = Left_Front_Btm;
                    }
                }
                else {
                    if (IsTop == POS_TOP) {
                        tag = Left_Back_Top;
                    }
                    else {
                        tag = Left_Back_Btm;
                    }
                }
            }
            else {
                if (IsFront == POS_FRONT) {
                    if (IsTop == POS_TOP) {
                        tag = Right_Front_Top;
                    }
                    else {
                        tag = Right_Front_Btm;
                    }
                }
                else {
                    if (IsTop == POS_TOP) {
                        tag = Right_Back_Top;
                    }
                    else {
                        tag = Right_Back_Btm;
                    }
                }
            }
            row = tag.Row;
            col = tag.Col;
            Margin = tag.Margin;

            Card.Margin = Margin;
            Grid.SetColumn(Card, col);
            Grid.SetRow(Card, row);
            Grid.SetColumnSpan(Card, 2);
            Grid.SetRowSpan(Card, 2);
        }

        /// <summary>
        /// 出力ウィンドウにタグを表示
        /// </summary>
        /// <param name="tags">タグリスト</param>
        public void Debug_ShowTag(List<Tag> tags) {
            StringBuilder res = new StringBuilder();
            int i = 0;
            res.Append("----) Debug_ShowTag (----\n");
            foreach (Tag tag in tags) {
                res.Append($"{i} : {tag.TagName}\n");
                i++;
            }
            res.Append("-------------------------");
            Debug.WriteLine($"{res}");
        }


        // カードを更新
        public static void Update_Cards_IsServe(string ServerTeam, int CountIndex) {

            // ここからサーブ・レシーブ判定
            StringBuilder S_SearchIndex = new StringBuilder();
            S_SearchIndex.Append("TEAM_");

            StringBuilder R_SearchIndex = new StringBuilder();
            R_SearchIndex.Append("TEAM_");
            string ReServerTeam = null;

            if (ServerTeam == TEAM_A) ReServerTeam = TEAM_B;
            else if (ServerTeam == TEAM_B) ReServerTeam = TEAM_A;

            string server = BASELINER;
            string re_server = BASELINER;
            string[] a = new string[] { BASELINER, BASELINER, VOLLEYER, VOLLEYER };

            int n = 0;
            if (CountIndex % 4 - 1 == -1) n = 3;
            else n = CountIndex % 4 - 1;
            server = a[n];
            
            for (int i = 0; i < CountIndex-1; i++) {
                if (re_server == BASELINER) {
                    re_server = VOLLEYER;
                }
                else if (re_server == VOLLEYER) {
                    re_server = BASELINER;
                }
            }
            // ここまでサーブ・レシーブ判定

            // ここからサーバー・レシーバーのキー(Dictionary)を指定するための文字列を生成
            S_SearchIndex.Append(ServerTeam);
            S_SearchIndex.Append("_");
            S_SearchIndex.Append(server);
            
            R_SearchIndex.Append(ReServerTeam);
            R_SearchIndex.Append("_");
            R_SearchIndex.Append(re_server);
            // ここまででキーは完成(S_SearchIndex, R_SearchIndex)
            ServerKey = S_SearchIndex.ToString();
            // ここからそれぞれのカードの設定を指定
            // 一旦すべてを基本の形に戻して...
            foreach(KeyValuePair<string,PlayerCard> c in Cards) {
                c.Value.Init(false, false);
            }
            // サーバー・レシーバーのカードだけ、もう一度(引数ありで)初期化
            Cards[S_SearchIndex.ToString()].Init(true, false);
            Cards[R_SearchIndex.ToString()].Init(false, true);

            // カードの位置を確定
            foreach (KeyValuePair<string, PlayerCard> c in Cards) {

                Set_CardPos_FromKey(c.Value, c.Key.ToString(), CountIndex);
            }
            // すべてのカードの更新完了!はもうすこししてから
        }

         static void Set_CardPos_FromKey(PlayerCard card, string key, int countIndex) {
            string IsLeft = SIDE_LEFT; // 画面上のサイドの位置
            string IsFront = POS_FRONT; // 前にいるか後ろにいるか
            string IsTop = POS_TOP; // 画面上で上かどうか

            if (key == TEAM_A_BASELINER) {
                IsLeft = Match.Side_A;
                IsFront = POS_BACK;
                // もしサーバーなら、1ptごとに入れ変わり
                if (card.IsServe_on_card) {
                    // 右側のサイドなら、1st->上, 2nd->下...
                    if(IsLeft == SIDE_RIGHT) {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_TOP;
                        }
                        else {
                            IsTop = POS_BOTTOM;
                        }
                    }
                    // 左側のサイドなら、1st->下, 2nd->上...
                    else {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_BOTTOM;
                        }
                        else {
                            IsTop = POS_TOP;
                        }
                    }
                }
                else if(Match.Server == TEAM_A) {
                    if (IsLeft == SIDE_RIGHT) {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_BOTTOM;
                        }
                        else {
                            IsTop = POS_TOP;
                        }
                    }
                    // 左側のサイドなら、1st->下, 2nd->上...
                    else {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_TOP;
                        }
                        else {
                            IsTop = POS_BOTTOM;
                        }
                    }
                }
                // レシーバーなら上下は固定
                else {
                    if (IsLeft == SIDE_RIGHT) {
                        IsTop = POS_TOP;   
                    }
                    else {
                        IsTop = POS_BOTTOM;
                    }
                }
            }
            else if(key == TEAM_A_VOLLEYER) {
                IsLeft = Match.Side_A;
                IsFront = POS_FRONT; // 基本前にいる(前衛だし)
                if (card.IsServe_on_card || card.IsReServe_on_card) {
                    IsFront = POS_BACK; // サーバーかレシーバーだったら後ろに行く
                }

                if (card.IsServe_on_card) {
                    // 右側のサイドなら、1st->上, 2nd->下...
                    if (IsLeft == SIDE_RIGHT) {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_TOP;
                        }
                        else {
                            IsTop = POS_BOTTOM;
                        }
                    }
                    // 左側のサイドなら、1st->下, 2nd->上...
                    else {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_BOTTOM;
                        }
                        else {
                            IsTop = POS_TOP;
                        }
                    }
                }
                else if(Match.Server == TEAM_A) {
                    if (IsLeft == SIDE_RIGHT) {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_BOTTOM;
                        }
                        else {
                            IsTop = POS_TOP;
                        }
                    }
                    // 左側のサイドなら、1st->下, 2nd->上...
                    else {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_TOP;
                        }
                        else {
                            IsTop = POS_BOTTOM;
                        }
                    }
                }
                // レシーバーなら上下は固定
                else {
                    if (IsLeft == SIDE_RIGHT) {
                        IsTop = POS_BOTTOM;
                    }
                    else {
                        IsTop = POS_TOP;
                    }
                }

            }
            else if (key == TEAM_B_BASELINER) {
                IsLeft = Match.Side_B;
                IsFront = POS_BACK;
                // もしサーブするチームがBなら、1ptごとに入れ変わり
                if (card.IsServe_on_card) {
                    // 右側のサイドなら、1st->上, 2nd->下...
                    if (IsLeft == SIDE_RIGHT) {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_TOP;
                        }
                        else {
                            IsTop = POS_BOTTOM;
                        }
                    }
                    // 左側のサイドなら、1st->下, 2nd->上...
                    else {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_BOTTOM;
                        }
                        else {
                            IsTop = POS_TOP;
                        }
                    }
                }
                else if (Match.Server == TEAM_B) {
                    if (IsLeft == SIDE_RIGHT) {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_BOTTOM;
                        }
                        else {
                            IsTop = POS_TOP;
                        }
                    }
                    // 左側のサイドなら、1st->下, 2nd->上...
                    else {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_TOP;
                        }
                        else {
                            IsTop = POS_BOTTOM;
                        }
                    }
                }
                // レシーバーなら上下は固定
                else {
                    if (IsLeft == SIDE_RIGHT) {
                        IsTop = POS_TOP;
                    }
                    else {
                        IsTop = POS_BOTTOM;
                    }
                }
            }
            else if (key == TEAM_B_VOLLEYER) {
                IsLeft = Match.Side_B;
                IsFront = POS_FRONT;
                if (card.IsServe_on_card || card.IsReServe_on_card) {
                    IsFront = POS_BACK; // サーバーかレシーバーだったら後ろに行く
                }
                if (card.IsServe_on_card) {
                    // 右側のサイドなら、1st->上, 2nd->下...
                    if (IsLeft == SIDE_RIGHT) {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_TOP;
                        }
                        else {
                            IsTop = POS_BOTTOM;
                        }
                    }
                    // 左側のサイドなら、1st->下, 2nd->上...
                    else {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_BOTTOM;
                        }
                        else {
                            IsTop = POS_TOP;
                        }
                    }
                }
                else if (Match.Server == TEAM_B) {
                    if (IsLeft == SIDE_RIGHT) {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_BOTTOM;
                        }
                        else {
                            IsTop = POS_TOP;
                        }
                    }
                    // 左側のサイドなら、1st->下, 2nd->上...
                    else {
                        if (countIndex % 2 == 1) {
                            IsTop = POS_TOP;
                        }
                        else {
                            IsTop = POS_BOTTOM;
                        }
                    }
                }
                // レシーバーなら上下は固定
                else {
                    if (IsLeft == SIDE_RIGHT) {
                        IsTop = POS_BOTTOM;
                    }
                    else {
                        IsTop = POS_TOP;
                    }
                }
            }

            Set_CardPos(card, IsTop, IsLeft, IsFront);
        }
    }

}
