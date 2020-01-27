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

namespace Recoder.Helpers {
    public class MatchHelper {

        public TextBlock tagText = new TextBlock()
        {
            Text = "",
            Margin = new Thickness(5, 10, 10, 10),
            FontSize = 12
        };

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
