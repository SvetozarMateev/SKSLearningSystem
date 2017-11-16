using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKSLearningSystem.Services
{
   public class JSONservices
    {
        public static void SaveToFile(Object obj)
        {
            string url = "../../Data/saved.json";
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(url, json);
        }
    }
}
