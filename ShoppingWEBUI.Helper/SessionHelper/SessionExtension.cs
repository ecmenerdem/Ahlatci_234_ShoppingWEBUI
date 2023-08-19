using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShoppingWEBUI.Helper.SessionHelper
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, string key, object? value)
        {
            var x = JsonConvert.SerializeObject(value, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            });
           
            session.SetString(key, x);
        }

        public static T? GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            
            return value==null?default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
