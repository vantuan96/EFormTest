using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace Admin.MemCached
{
    //public static class CacheHelper
    //{
    //    public static MemoryCache entityCache = MemoryCache.Default;
    //    public static object GetObject(string sKey)
    //    {
    //        return entityCache.Get(sKey);
    //    }
    //    public static void AddObject(object entity, string sKey)
    //    {
    //        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
    //        cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddDays(90);

    //        entityCache.Add(sKey, entity, cacheItemPolicy);
    //    }
    //    public static void AddObject(object entity, string sKey, DateTime dExpireTime)
    //    {
    //        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
    //        cacheItemPolicy.AbsoluteExpiration = dExpireTime;
    //        entityCache.Add(sKey, entity, cacheItemPolicy);
    //    }
    //    public static void Set(object entity, string sKey)
    //    {
    //        entityCache.Set(sKey, entity, new CacheItemPolicy());
    //    }
    //    public static void RemoveBy(string sKey)
    //    {
    //        entityCache.Remove(sKey);
    //    }
    //    public static void RemoveAll()
    //    {
    //        entityCache.Dispose();
    //    }
    //}
}