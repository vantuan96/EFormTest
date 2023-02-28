using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Caching;
using System.Web;

namespace Admin.MemCached
{
    //public class LoginFailed
    //{
    //    private static MemoryCache cache = MemoryCache.Default;

    //    public static bool Store(string ip, int time)
    //    {
    //        var absExpiration = DateTimeOffset.UtcNow.AddDays(1);
    //        if (cache.Contains(ip))
    //        {
    //            cache.Remove(ip);
    //        }
    //        return cache.Add(ip, time, absExpiration);
    //    }

    //    public static int Get(string ip)
    //    {
    //        try
    //        {
    //            var x = Int32.Parse(cache.Get(ip).ToString());
    //            return x;
    //        }
    //        catch (Exception)
    //        {
    //            return 0;
    //        }
    //    }

    //    public static bool Add(string ip)
    //    {
    //        int time = Get(ip) + 1;
    //        return Store(ip, time);
    //    }

    //    public static bool Remove(string ip)
    //    {
    //        if (cache.Contains(ip))
    //        {
    //            cache.Remove(ip);
    //        }
    //        return true;
    //    }
    //}
}