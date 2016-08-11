using Newtonsoft.Json;
using OPM.Core.Config;
using StackExchange.Redis;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPM.Core.Cache
{
    public class RedisCache : IOPMSessionCache
    {
        public RedisCache(OPMConfig config)
        {
            _cacheConnetionString = config.CacheConnectionString;
            _conn = ConnectionMultiplexer.Connect(_cacheConnetionString);
            _db = _conn.GetDatabase(0);
        }

        string _cacheConnetionString;
        ConnectionMultiplexer _conn;
        IDatabase _db;

     /// <summary>
     /// 
     /// </summary>
     /// <typeparam name="T"></typeparam>
     /// <param name="sid"></param>
     /// <param name="key"></param>
     /// <returns></returns>
        public virtual object Get<T>(string sid, string key)
        {
            if (typeof(T).IsPrimitive || typeof(T).Name == "String")
            {
              return   GetString(sid,key);
            }
            else
            {
                return GetItem<T>(sid, key);
            }

        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="sid"></param>
       /// <param name="key"></param>
       /// <param name="data"></param>
       /// <param name="cacheTime"></param>
        public virtual void Set(string sid, string key, object data, int cacheTime = 15)
        {
            SetItem(sid, key, data, TimeSpan.FromMinutes(cacheTime));
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public virtual bool IsSet(string key)
        {
            return _db.KeyExists(key);
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            _db.KeyDelete(key);
        }

        #region private

        private bool SetObject<T>(string key, T obj, TimeSpan? expiry = default(TimeSpan?))
        {
            string json = JsonConvert.SerializeObject(obj);
            return _db.StringSet(key, json, expiry);

        }
        private T GetObject<T>(string key)
        {
            return JsonConvert.DeserializeObject<T>(_db.StringGet(key));
        }

        private bool KeyDelete(string key)
        {
            return _db.KeyDelete(key);
        }

        private void SetItem(string sid, string key, Object value, TimeSpan? expiry = default(TimeSpan?))
        {
            Hashtable ht = new Hashtable();
            if (IsSet(sid))
            {
                ht = GetObject<Hashtable>(sid);
                KeyDelete(sid);
            }

            if (ht.ContainsKey(key))
            {
                ht[key] = value;
            }
            else
            {
                ht.Add(key, value);
            }

            SetObject(sid, ht, expiry);

        }

        private T GetItem<T>(string sid, string key)
        {
            if (!IsSet(sid)) return default(T);
            var ht = GetObject<Hashtable>(sid);
            if (ht != null && ht.ContainsKey(key))
            {
                return JsonConvert.DeserializeObject<T>(ht[key].ToString());
            }
            return default(T);
        }

        private string GetString(string sid, string key)
        {
            if (!IsSet(sid)) return string.Empty;
            var ht = GetObject<Hashtable>(sid);
            if (ht != null && ht.ContainsKey(key))
            {
                return ht[key].ToString();
            }
            return string.Empty; ;
        }
        #endregion

        public virtual void Dispose()
        {

        }
    }
}
