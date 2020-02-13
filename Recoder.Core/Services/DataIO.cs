using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

using Windows.Storage;

using Recoder.Core;
using System.Diagnostics;

namespace Recoder.Core.Services {
    public class DataIO {
        static public void TestOutput() {
            string output = JsonConvert.SerializeObject(SampleMatchService.test);
            Debug.WriteLine(output);
        }

        static public void JsonOutput(Object obj) {
            if (obj != null) {
                string output = JsonConvert.SerializeObject(obj);
                Debug.WriteLine(output);
            }
        }

        static public void TestInput(object json) {
            var deserialized = JsonConvert.DeserializeObject<Core.Models.MatchData>(json.ToString());
        }

        static async public void TestText() {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
             
        }
    }
}
