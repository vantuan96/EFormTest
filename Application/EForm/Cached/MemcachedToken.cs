using Enyim.Caching;
using Enyim.Caching.Memcached;
using System;
using System.Diagnostics;

namespace EForm.MemCached
{
    public class MemcachedToken
    {
        private static MemcachedClient client = new MemcachedClient();

        public static bool StoreInBlackList(string token)
        {
            var timeout = new TimeSpan(1, 0, 0, 0);
            try
            {
                return client.Store(StoreMode.Set, token.Substring(0, 10), token, timeout);
            }catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public static bool IsTokenInBlackList(string token)
        {
            try
            {
                string value = client.Get<string>(token.Substring(0, 10));
                if (value == token)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

    }
}