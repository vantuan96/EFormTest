using System;
using System.Runtime.Caching;

namespace Admin.MemCached
{
    //public class SpamAD
    //{
    //    private static MemoryCache cache = MemoryCache.Default;

    //    public static bool Store(string ip, int time)
    //    {
    //        var absExpiration = DateTime.UtcNow.AddMinutes(2);
    //        if (cache.Contains($"[AD]{ip}"))
    //        {
    //            cache.Remove($"[AD]{ip}");
    //        }
    //        return cache.Add($"[AD]{ip}", time, absExpiration);
    //    }

    //    public static int Get(string ip)
    //    {
    //        try
    //        {
    //            var x = Int32.Parse(cache.Get($"[AD]{ip}").ToString());
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
    //        if(time > 11)
    //            return false;
    //        return Store(ip, time);
    //    }

    //    public static bool Remove(string ip)
    //    {
    //        if (cache.Contains($"[AD]{ip}"))
    //        {
    //            cache.Remove($"[AD]{ip}");
    //        }
    //        return true;
    //    }
    //}
}