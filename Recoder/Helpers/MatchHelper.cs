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

namespace Recoder.Helpers {
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
            if (type == "Base") {
                if (CountA >= 10) {
                    return CountA.ToString() + " - 0" + CountB.ToString();
                }
                else if (CountB >= 10) {
                    return "0" + CountA.ToString() + " - " + CountB.ToString();
                }
            }
            else if (type == "SoftTennis") {
                if (CountA > CountB && CountA >= 4) {
                    return "Adv - " + CountB.ToString();
                }
                else if (CountB > CountA && CountB >= 4) {
                    return CountA.ToString() + " - Adv";
                }
            }
            else if (type == "SoftTennis_Final") {
                if (CountA > CountB && CountA >= 6) {
                    return "Adv - " + CountB.ToString();
                }
                else if (CountB > CountA && CountB >= 6) {
                    return CountA.ToString() + " - Adv";
                }
            }
            return CountA.ToString() + " - " + CountB.ToString();
        }

        /// <summary>
        /// プレイヤーカードのセットアップ
        /// </summary>
        /// <param name="NewPlayerName">プレイヤーの名前</param>
        /// <param name="NewTeamName">チームの名前</param>
        /// <param name="ServeOrReServe">サーバー:"Server", レシーバー:"ReServer"</param>
        /// <param name="position">前衛:"volley", 後衛:"baseliner"</param>
        /// <param name="TeamIndex">チームのサイド選択 "A"or"B"</param>
        public PlayerCard Set_PlayerCard(
            // TODO: InitilizeComponentを先に行うか後に行うか検討。後のほうが望ましい
            string NewPlayerName,
            string NewTeamName,
            string ServeOrReServe,
            string position,
            string TeamIndex) {
            PlayerCard card = new PlayerCard();
            card.InitializeComponent();
            Init_Card(card, NewTeamName, NewPlayerName, ServeOrReServe);
            Set_Card_Position(card, position, TeamIndex);
            return card;
        }

        /// <summary>
        /// カードの初期化
        /// </summary>
        private void Init_Card(PlayerCard Card, string TName, string PName, string pos) {
            bool Serve = false, ReServe = false;
            if (pos == "Serve") Serve = true;
            else if (pos == "ReServe") ReServe = true;
            Card.Init(Serve, ReServe);
            Card.SetPlayerName(PName);
            Card.SetTeamName(TName);
            Card.Width = 150;
            Card.Height = 220;
        }

        /// <summary>
        /// カードのポジション設定
        /// </summary>
        public void Set_Card_Position(PlayerCard Card, string pos, string Team) {
            var IsFront = false;
            int row = 0;
            int col = 0;
            if (pos == "volley") IsFront = true;
            else if (pos == "baseliner") IsFront = true;
            else // エラーハンドリング

            if (Team == "A") {
                if (IsFront) (row, col) = (0, 1);
                else row = 1;
            }
            else if (Team == "B") {
                if (IsFront) (row, col) = (1, 3);
                else (row, col) = (0, 4);
            }
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
