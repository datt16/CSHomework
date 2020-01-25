using System;
using System.Collections.Generic;

namespace Recoder.Core.Models {
    public class Point {
        public int Id { get; set; }
        public char Getter { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
