using System;

namespace EForm.Cached
{
    public class CachedSession
    {
        public static bool StoreInBlackList(string session)
        {
            try
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                var key = session.Substring(0, 30);
                var absExpiration = TimeSpan.FromDays(1);
                if (cache.KeyExists(key))
                    cache.KeyDelete(key);
                return cache.StringSet(key, session, absExpiration);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool IsTokenInBlackList(string session)
        {
            try
            {
                var cache = RedisConnectorHelper.Connection.GetDatabase();
                string value = cache.StringGet(session.Substring(0, 30));
                if (value == session)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}