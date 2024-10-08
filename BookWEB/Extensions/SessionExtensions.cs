﻿using Newtonsoft.Json;

namespace BookWEB.Extensions
{
    public  static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key,JsonConvert.SerializeObject(value));

        }
        public static T GetObjectFromJson<T> (this ISession session, string key) where T : class
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
