using System;
using System.Collections.Generic;
using System.Text;
using Recoder.Core.Models;

namespace Recoder.Core.Services {
    public class BasicTag {
        public Tag Fault = new Tag()
        {
            ShortTag = "F",
            DisplayName = "F",
            Description = "読み方:フォールト\nサーブに失敗するとフォールトになります。",
            TagName = "Fault"
        };
        public Tag WFault = new Tag()
        {
            ShortTag = "WF",
            DisplayName = "WF",
            Description = "読み方:ダブルフォールト\nサーブに2回失敗するとダブルフォールトになります。\nレシーブ側に1点追加されます",
            TagName = "WFault"
        };
        public Tag Ace = new Tag()
        {
            ShortTag = "A",
            DisplayName = "Ace",
            TagName = "Ace"
        };
        public Tag ServiceAce = new Tag()
        {
            ShortTag = "SA",
            DisplayName = "SAce",
            TagName = "ServiceAce"
        };
        public Tag FootFault = new Tag()
        {
            ShortTag = "FF",
            DisplayName = "FF",
            TagName = "FootFault"
        };
        public Tag ServiceNet = new Tag()
        {
            ShortTag = "SN",
            DisplayName = "NF",
            TagName = "ServiceNet"
        };
        public Tag Net = new Tag()
        {
            ShortTag = "N",
            DisplayName = "N",
            TagName = "Net"
        };
        public Tag Out = new Tag()
        {
            ShortTag = "O",
            DisplayName = "Out",
            TagName = "Out"
        };
        public Tag TwoBounds = new Tag()
        {
            ShortTag = "2B",
            DisplayName = "2B",
            TagName = "TwoBounds"
        };
    }

    public class PositonTag {
        public static Tag Volleyer = new Tag()
        {
            ShortTag = "Front",
            DisplayName = "前衛",
            TagName = "Volleyer"
        };

        public static Tag Baseliner = new Tag()
        {
            ShortTag = "Back",
            DisplayName = "後衛",
            TagName = "BaseLiner"
        };

        public static Tag Neutral = new Tag()
        {
            ShortTag = "Neut",
            DisplayName = "",
            TagName = "Neutral"
        };
    }
}
