using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Recoder.Helpers {
    public static class Control_Alias {
        public static readonly string SERVE = "Serve";
        public static readonly string NONE = "None";
        public static readonly string RE_SERVE = "ReServe";
        public static readonly string TYPE_ST_FINAL = "SoftTennis_Final";
        public static readonly string TYPE_ST = "SoftTennis";
        public static readonly string TYPE_BASIC = "Base";
        public static readonly string TEAM_A = "A";
        public static readonly string TEAM_B = "B";
        public static readonly string SIDE_LEFT = "Left";
        public static readonly string SIDE_RIGHT = "Right";
        public static readonly string POS_TOP = "Top";
        public static readonly string POS_BOTTOM = "Bottom";
        public static readonly string POS_FRONT = "Front";
        public static readonly string POS_BACK = "Back";
    }
    public static class Positon_Alias {

        public static Card_Positon Left_Front_Top = new Card_Positon()
        {
            Id = "Left_Front_Top",
            Row = 0,
            Col = 1
        };

        public static Card_Positon Left_Front_Btm = new Card_Positon()
        {
            // Grid.Column="1" Grid.Row="1" Margin="0,0,0,30"
            Id = "Left_Front_Btm",
            Row = 1,
            Col = 1,
            Margin = new Thickness(0, 0, 0, 30)
        };

        public static Card_Positon Left_Back_Top = new Card_Positon()
        {
            Id = "Left_Back_Top",
            Row = 0,
            Col = 0,
            Margin = new Thickness(100, 0, 0, 0)
        };

        public static Card_Positon Left_Back_Btm = new Card_Positon()
        {
            // Grid.Column="0" Grid.Row="1"
            Id= "Left_Back_Btm",
            Row = 1,
            Col = 0
        };

        public static Card_Positon Right_Front_Top = new Card_Positon()
        {
            Id = "Right_Front_Top",
            Row = 0,
            Col = 3
        };

        public static Card_Positon Right_Front_Btm = new Card_Positon()
        {
            Id = "Right_Front_Btm",
            Row = 1,
            Col = 3,
        };

        public static Card_Positon Right_Back_Top = new Card_Positon()
        {
            Id = "Right_Back_Top",
            Row = 0,
            Col = 4,
        };

        public static Card_Positon Right_Back_Btm = new Card_Positon()
        {
            Id = "Right_Back_Btm",
            Row = 1,
            Col = 4
        };
    }

    public class Card_Positon {

        public  string Id { get; set; }

        public int Row { get; set; } = 0;

        public int Col { get; set; } = 0;

        public Thickness Margin { get; set; } = new Thickness(0, 0, 0, 0);
    }
}
