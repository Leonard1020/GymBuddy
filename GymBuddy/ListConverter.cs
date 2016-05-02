using System.Collections.Generic;
using Newtonsoft.Json;

namespace GymBuddy
{
    internal class ListConverter
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };

        public static string ConvertListToString<T>(IList<T> list)
        {
            return JsonConvert.SerializeObject(list ?? new List<T>(), Settings);
        }

        public static List<T> ConvertStringToList<T>(string list)
        {
            return string.IsNullOrWhiteSpace(list) ? new List<T>() : JsonConvert.DeserializeObject<List<T>>(list, Settings);
        }
    }
}