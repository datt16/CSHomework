﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using Recoder.Core;
using System.Diagnostics;

namespace Recoder.Core.Services {
    public class DataIO {

        static public void JsonOutput(Object obj) {
            if (obj != null) {
                string output = JsonConvert.SerializeObject(obj);
                Debug.WriteLine(output);
            }
        }

        static public void TestInput(object json) {
            var deserialized = JsonConvert.DeserializeObject<Core.Models.MatchData>(json.ToString());
        }
    }
}
