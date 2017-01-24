using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboMart.Common.Helpers
{
    public static class JsonHelper
    {
        public static T ConverToObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static string ConverToJson<T>(T @object)
        {
            return JsonConvert.SerializeObject(@object, Formatting.Indented);
        }

    }
}
