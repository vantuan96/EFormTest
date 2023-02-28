using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.MemCached
{
    //public class SessionBlackList
    //{
    //    private static MemcachedClient client = new MemcachedClient();

    //    public static bool Store(string session)
    //    {
    //        var timeout = new TimeSpan(1, 0, 0, 0);
    //        try
    //        {
    //            return client.Store(StoreMode.Set, session.Substring(0, 20), session, timeout);
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //    }

    //    public static bool IsTrue(string session)
    //    {
    //        try
    //        {
    //            string value = client.Get<string>(session.Substring(0, 20));
    //            if (value == session)
    //            {
    //                return true;
    //            }
    //            return false;
    //        }
    //        catch (Exception ex)
    //        {
    //            return false;
    //        }
    //    }
    //}
}