﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using StationCAD.Model.OneSignal;

namespace StationCAD.Model.Helpers
{
    public static class JsonUtil <T>
        where T : AbstractJsonEntity
    {

        public static string ToJson(T entity)
        {
            return JsonConvert.SerializeObject(entity,Formatting.Indented);
        }

        public static T FromJson(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static ICollection<T> FromJsonCollection(string json)
        {
            return JsonConvert.DeserializeObject<ICollection<T>>(json);
        }
    }
}
