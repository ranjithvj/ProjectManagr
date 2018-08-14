using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Repositories.Cache
{
    public class CacheHelper
    {
        private static CacheHelper _instance;
        private bool _needExpiry;
        private short _expiryTime;
        private ConcurrentDictionary<string, ConcurrentDictionary<int, object>> _cachedObjects;

        private CacheHelper()
        {
            _cachedObjects = new ConcurrentDictionary<string, ConcurrentDictionary<int, object>>();
        }

        public static CacheHelper GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CacheHelper();
                _instance.InitiateTimer();
                return _instance;
            }
            return _instance;
        }

        private void InitiateTimer()
        {
            _needExpiry = Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["NeedExpiry"].ToLower());
            _expiryTime = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["ExpiryTime"]);
            if (_needExpiry)
            {
                System.Timers.Timer aTimer = new System.Timers.Timer();
                aTimer.Elapsed += new ElapsedEventHandler(_instance.ClearCache);
                aTimer.Interval = _expiryTime*60*60*1000;
                aTimer.Enabled = true;
            }
        }

        public void ClearCache(Object myObject, EventArgs myEventArgs)
        {
            _cachedObjects = new ConcurrentDictionary<string, ConcurrentDictionary<int, object>>();
        }

        public List<T> GetAll<T>()
        {
            var resultList = new ConcurrentDictionary<int, object>();
            if (_cachedObjects.TryGetValue(typeof(T).Name, out resultList))
            {
                return resultList.Values.Cast<T>().ToList();
            }
            return null;
        }
        public T GetById<T>(int itemId)
        {
            ConcurrentDictionary<int, dynamic> requiredList = null;
            bool isPresent = _cachedObjects.TryGetValue(typeof(T).Name, out requiredList);
            if (isPresent)
            {
                dynamic requiredValue = null;
                if (requiredList.TryGetValue(itemId, out requiredValue))
                {
                    return (T)requiredValue;
                }
            }
            //TODO: fetch from db & update cache
            return default(T);
        }

        public void AddOrUpdate<T>(int itemId, object item)
        {
            ConcurrentDictionary<int, object> requiredList = null;
            if (_cachedObjects.TryGetValue(typeof(T).Name, out requiredList))
            {
                requiredList.AddOrUpdate(itemId, item, (key, value) => value);

            }
            else
            {
                var listToBeAdded = new ConcurrentDictionary<int, dynamic>();
                listToBeAdded.TryAdd(itemId, item);
                _cachedObjects.TryAdd(typeof(T).Name, listToBeAdded);
            }
        }
        public void DeleteItem<T>(int itemId)
        {
            ConcurrentDictionary<int, object> requiredList = null;
            if (_cachedObjects.TryGetValue(typeof(T).Name, out requiredList))
            {
                object value = null;
                requiredList.TryRemove(itemId, out value);
            }
        }
    }
}