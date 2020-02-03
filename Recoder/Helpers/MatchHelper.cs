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
        public static PlayerCard Set_PlayerCard(Player player, string NewTeamName, string IsServe, string Side, string yPos, string xPos) {
            NullCheck(player, NewTeamName, IsServe, Side);
            PlayerCard card = new PlayerCard();
            card.InitializeComponent();

            string NewPlayerName = player.Name;

            Init_Card(card, NewTeamName, NewPlayerName, IsServe);
            Set_Card_Position(card, yPos, Side, xPos);

            return card;
        }

        /// <summary>
        /// カードの初期化
        /// </summary>
        private static void Init_Card(PlayerCard Card, string TName, string PName, string pos) {
            bool Serve = false, ReServe = false;
            if (pos == SERVE) Serve = true;
            else if (pos == RE_SERVE) ReServe = true;
            Card.Init(Serve, ReServe);
            Card.SetPlayerName(PName);
            Card.SetTeamName(TName);
            Card.Width = 150;
            Card.Height = 220;
        }

        /// <summary>
        /// カードのポジション設定
        /// </summary>
        public static void Set_Card_Position(PlayerCard Card, string IsTop, string IsLeft, string IsFront) {
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
    }

}
