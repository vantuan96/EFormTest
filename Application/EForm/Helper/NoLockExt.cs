using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Transactions;
using System.Web;

namespace EForm.Helper
{
    public static class NoLockExtension
    {
        public static IQueryable<T> ToIQueryableWithNoLock<T>(this IQueryable<T> enumValue)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                return enumValue;
            }
        }

        public static System.Transactions.TransactionScope GetNewReadUncommittedScope()
        {
            var timeout_config = ConfigurationManager.AppSettings["TRANSACTION_TIMEOUT"];
            var timeout = TimeSpan.FromMinutes(int.Parse(string.IsNullOrEmpty(timeout_config) ? "10" : timeout_config.ToString()));
            return new System.Transactions.TransactionScope(
                System.Transactions.TransactionScopeOption.RequiresNew,
                new System.Transactions.TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                    Timeout = timeout
                });
        }

        public static List<T> ToListNoLock<T>(this IQueryable<T> query)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                return query.ToList();
            }
        }
        public static int CountNoLock<T>(this IQueryable<T> query)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                return query.Count();
            }
        }
        public static T FirstOrDefaultWithNoLock<T>(this IQueryable<T> query)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                return query.FirstOrDefault();
            }
        }
        public static U NoLock<T, U>(this IQueryable<T> query, Func<IQueryable<T>, U> expr)
        {
            using (var txn = GetNewReadUncommittedScope())
            {
                return expr(query);
            }
        }
    }
}