using System;
using System.Collections.Concurrent;
using System.Runtime.Caching;

namespace GreenUtil.Data
{
    /// <summary>
    /// Cache utilities
    /// </summary>
    public static class CacheUtil
    {
        private static ConcurrentDictionary<string, byte> concurrentDictionary;

        private static int defaultExpiration = 10;

        /// <summary>
        /// Time offset for cache entries expiration (in minutes)
        /// </summary>
        public static int DefaultExpiration
        {
            get { return defaultExpiration; }
            set
            {
                lock (concurrentDictionary) { defaultExpiration = value; }
            }
        }

        /// <summary>
        /// Cache elements count
        /// </summary>
        public static int Count
        {
            get { return concurrentDictionary.Count; }
        }

        /// <summary>
        /// CacheUtil constructor
        /// </summary>
        static CacheUtil()
        {
            concurrentDictionary = new ConcurrentDictionary<string, byte>();
        }

        /// <summary>
        /// Gets or set an item from/into cache using a delegate for evaluation. If the key exists in cache the value is returned, otherwise the cache funciontion callback is invoked and the return of the function cached.
        /// </summary>
        /// <typeparam name="T">Function return type</typeparam>
        /// <param name="key">Key to store in cache</param>
        /// <param name="cacheCallback">Callback function when key is not present in dictionary</param>
        /// <returns></returns>
        public static T GetOrSet<T>(string key, Func<T> cacheCallback) where T : class
        {
            if (cacheCallback == null)
                throw new ArgumentNullException(nameof(cacheCallback));

            T item = MemoryCache.Default.Get(key) as T;

            if (item == null)
            {
                item = cacheCallback();
                MemoryCache.Default.Add(key, item, DateTime.Now.AddMinutes(DefaultExpiration));
                concurrentDictionary.GetOrAdd(key, byte.MinValue);
            }

            return item;
        }

        /// <summary>
        /// Invalidate the whole cache
        /// </summary>
        public static void InvalidateAll()
        {
            foreach (var item in concurrentDictionary)
            {
                MemoryCache.Default.Remove(item.Key);
                concurrentDictionary.TryRemove(item.Key, out byte value);
            }
        }

        /// <summary>
        /// Invalidate all keys thar are likely(contains) the informed as parameter
        /// </summary>
        /// <param name="likeKey"></param>
        public static void InvalidateSimilar(string likeKey)
        {
            foreach (var item in concurrentDictionary)
            {
                if (item.Key.Contains(likeKey))
                {
                    MemoryCache.Default.Remove(item.Key);
                    concurrentDictionary.TryRemove(item.Key, out byte value);
                }
            }
        }
    }
}
