using System;
using System.Collections.Generic;
using System.Text;

namespace Recoder.Core.Models {
    public class Tag {
        public string ShortTag { set; get; }

        public string Description { get; set; } = "説明なし";

        public string DisplayName { set; get; }

        public string TagName { set; get; }
    }
}
