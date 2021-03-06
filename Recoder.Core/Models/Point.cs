﻿using System;
using System.Collections.Generic;

namespace Recoder.Core.Models {

    public class Point {

        public int Id { get; set; }

        public bool IsSpecial { get; set; } = false;

        public string Getter { get; set; }

        public int Rally { get; set; } = 0;

        public List<Tag> Tags { get; set; }
    }
}
