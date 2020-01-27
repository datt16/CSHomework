using System;
using System.Collections.Generic;
using System.Text;
using Recoder.Core.Models;

namespace Recoder.Core.Services {
    public class BasicTag {
        public Tag Fault = new Tag()
        {
            ShortTag = "F",
            TagName = "Fault"
        };
        public Tag WFault = new Tag()
        {
            ShortTag = "WF",
            TagName = "WFault"
        };
        public Tag Ace = new Tag()
        {
            ShortTag = "A",
            TagName = "Ace"
        };
        public Tag ServiceAce = new Tag()
        {
            ShortTag = "SA",
            TagName = "Ace"
        };
        public Tag FootFault = new Tag()
        {
            ShortTag = "FF",
            TagName = "FootFault"
        };
        public Tag ServiceNet = new Tag()
        {
            ShortTag = "SN",
            TagName = "ServiceNet"
        };
        public Tag Net = new Tag()
        {
            ShortTag = "N",
            TagName = "Net"
        };
        public Tag Out = new Tag()
        {
            ShortTag = "O",
            TagName = "Out"
        };
        public Tag TwoBounds = new Tag()
        {
            ShortTag = "2B",
            TagName = "TwoBounds"
        };
    }
}
