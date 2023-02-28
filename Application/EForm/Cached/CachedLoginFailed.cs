using System;

namespace EForm.Cached
{
    public class CachedLoginFailed
    {
        public static bool Store(string ip, int time)
        {
            try
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                var absExpiration = TimeSpan.FromDays(1);
                if (cache.KeyExists(ip))
                    cache.KeyDelete(ip);
                return cache.StringSet(ip, time.ToString(), absExpiration);
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public static int Get(string ip)
        {
            try
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                var x = Int32.Parse(cache.StringGet(ip));
                return x;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static bool Add(string ip)
        {
            int time = Get(ip) + 1;
            return Store(ip, time);
        }

        public static bool Remove(string ip)
        {
            try
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                if (cache.KeyExists(ip))
                    cache.KeyDelete(ip);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}