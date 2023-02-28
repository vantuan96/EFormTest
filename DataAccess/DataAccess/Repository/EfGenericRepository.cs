﻿using DataAccess.Model.BaseModel;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Web;

namespace DataAccess.Repository
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal EmergencyDepartmentContext _context;
        internal DbSet<T> _dbSet;

        public EfGenericRepository(EmergencyDepartmentContext context)
        {
            this._context = context;
            this._dbSet = context.Set<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public IEnumerable<T> AsEnumerable()
        {
            return _dbSet;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T GetById(Guid id)
        {
            return _dbSet.Find(id);
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Single(predicate);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.First(predicate);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Count(predicate);
        }

        public DbQuery<T> Include(string path)
        {
            return _dbSet.Include(path);
        }

        public void Add(T entity)
        {
            if (entity is IEntity)
            {
                if (((IEntity)entity).Id == Guid.Empty)
                    ((IEntity)entity).Id = Guid.NewGuid();
                var userName = GetUserName();
                ((IEntity)entity).IsDeleted = false;
                
                if (string.IsNullOrWhiteSpace(((IEntity)entity).CreatedBy))
                    ((IEntity)entity).CreatedBy = userName;

                if (((IEntity)entity).UpdatedAt == null)
                    ((IEntity)entity).CreatedAt = DateTime.Now;

                if (string.IsNullOrWhiteSpace(((IEntity)entity).UpdatedBy))
                    ((IEntity)entity).UpdatedBy = userName;

                if (((IEntity)entity).UpdatedAt == null)
                    ((IEntity)entity).UpdatedAt = DateTime.Now;
            }
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (entity is IEntity)
            {
                var userName = GetUserName();
                ((IEntity)entity).IsDeleted = true;
                ((IEntity)entity).DeletedBy = userName;
                ((IEntity)entity).DeletedAt = DateTime.Now;
                ((IEntity)entity).UpdatedBy = userName;
                ((IEntity)entity).UpdatedAt = DateTime.Now;
            }
        }

        public void HardDelete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void HardDeleteRange(IQueryable<T> need_remove)
        {
            _dbSet.RemoveRange(need_remove);
        }

        public void Update(T entity, bool is_anonymous = false, bool is_time_change = true)
        {
            if (!(entity is IEntity))
                return;

            if (!is_anonymous)
            {
                var userName = GetUserName();
                ((IEntity)entity).UpdatedBy = userName;
            }

            if (is_time_change)
                ((IEntity)entity).UpdatedAt = DateTime.Now;
        }

        private string GetUserName()
        {
            try
            {
                // var idp = GetIp();
                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                return claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name, StringComparison.OrdinalIgnoreCase))?.Value;
            }
            catch (Exception) {
                // for dev
                if (ConfigurationManager.AppSettings["HiddenError"].Equals("true")) return null;
                var ip = GetIp();
                if (ConfigurationManager.AppSettings["DevWriteLists"].Contains(ip))
                {
                    return GetUserDev(ip);
                }
                return "";
            }
        }
        private string GetUserDev(string ip)
        {
            
            string[] userNames = ConfigurationManager.AppSettings.Get("DEV_ACCOUNT").Split(',');
            var user_name_from_webconfig = userNames.FirstOrDefault();
            if (ip == "10.115.88.239") return "hunglq25";
            if (ip == "10.115.88.70") return "ducdv11";
            if (ip == "10.115.50.133") return "thangdc3";
            if (ip == "10.115.90.32") return "thanhnt135";
            if (ip == "10.115.88.157") return "haulv4";
            if (ip == "10.115.88.229") return "tungpa1";
            if (ip == "::1")
            {
                // đọc code xem config từ chỗ nào di mấy bố, có 7 dòng thôi, đừng sửa chỗ này nữa
                return user_name_from_webconfig;
            }
            return "";
        }
        private string GetIp()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            if (context != null)
            {
                return context.Request?.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
    }
}
