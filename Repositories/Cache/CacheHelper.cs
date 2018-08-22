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
        private ConcurrentDictionary<string, Lazy<ConcurrentDictionary<int, object>>> _cachedObjects;

        private CacheHelper()
        {
            _cachedObjects = new ConcurrentDictionary<string, Lazy<ConcurrentDictionary<int, object>>>();
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
                aTimer.Interval = _expiryTime * 60 * 60 * 1000;
                aTimer.Enabled = true;
            }
        }

        public void ClearCache(Object myObject, EventArgs myEventArgs)
        {
            _cachedObjects = new ConcurrentDictionary<string, Lazy<ConcurrentDictionary<int, object>>>();
        }

        public List<T> GetOrAddAll<T>(Func<ConcurrentDictionary<int, object>> cacheMissMethod)
        {
            var allItems = _cachedObjects.GetOrAdd(typeof(T).Name,
                 new Lazy<ConcurrentDictionary<int, object>>(cacheMissMethod));

            return allItems?.Value?.Values?.Cast<T>()?.ToList() ?? new List<T>();
        }

        public T GetById<T>(int itemId, Func<ConcurrentDictionary<int, object>> cacheMissMethod)
        {
            //Load the parent Dictionary
            //Make sure all the records are present
            Lazy<ConcurrentDictionary<int, object>> requiredList = _cachedObjects.GetOrAdd(typeof(T).Name,
                new Lazy<ConcurrentDictionary<int, object>>(cacheMissMethod));

            object requiredValue = null;
            if (requiredList.Value.TryGetValue(itemId, out requiredValue))
            {
                return (T)requiredValue;
            }

            return default(T);
        }

        public void AddOrUpdate<T>(int itemId, object item , Func<ConcurrentDictionary<int, object>> cacheMissMethod)
        {
            //Scenario :
            //When the cache has been invalidated, and the user is trying to add data
            //GetOrAdd will first retreive the data from DB and hold in cache
            Lazy<ConcurrentDictionary<int, object>> requiredList = _cachedObjects.GetOrAdd(typeof(T).Name,
                new Lazy<ConcurrentDictionary<int, object>>(cacheMissMethod));

            //Test by clearing out the tables
            //Check if an empty  list is created or is it null
            requiredList.Value.AddOrUpdate(itemId, item, (key, value) => item);
        }
        
        public void DeleteItem<T>(int itemId, Func<ConcurrentDictionary<int, object>> cacheMissMethod)
        {
            Lazy<ConcurrentDictionary<int, object>> requiredList = _cachedObjects.GetOrAdd(typeof(T).Name,
             new Lazy<ConcurrentDictionary<int, object>>(cacheMissMethod));

            object removedItem = null;
            requiredList.Value.TryRemove(itemId, out removedItem);
        }
    }
}