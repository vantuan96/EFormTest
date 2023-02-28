using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataAccess.Dapper;
using DataAccess.Model.BaseModel;

namespace DataAccess.Dapper.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly string _tableName;
        private IDbConnection Connection
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["EmergencyDepartmentContext"].ConnectionString;
                return new SqlConnection(connectionString);
            }
        }
        public GenericRepository(string tableName)
        {
            _tableName = tableName;
        }
        internal virtual dynamic Mapping(T item)
        {
            return item;
        }
        public void Add(T entity)
        {
            if (entity is IEntity)
            {
                if (((IEntity)entity).Id == Guid.Empty)
                    ((IEntity)entity).Id = Guid.NewGuid();
                var userName = GetUserName();
                ((IEntity)entity).IsDeleted = false;
                ((IEntity)entity).CreatedBy = userName;
                ((IEntity)entity).CreatedAt = DateTime.Now;
                ((IEntity)entity).UpdatedBy = userName;
                ((IEntity)entity).UpdatedAt = DateTime.Now;
            }
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(entity);
                cn.Open();
                cn.Insert<Guid>(_tableName, parameters);
            }
        }
        public void Adds(List<T> entitys)
        {
            DateTime currentDate = DateTime.Now;
            foreach (T entity in entitys)
            {
                currentDate = currentDate.AddMilliseconds(10);
                if (((IEntity)entity).Id == Guid.Empty)
                    ((IEntity)entity).Id = Guid.NewGuid();
                var userName = GetUserName();
                ((IEntity)entity).IsDeleted = false;
                ((IEntity)entity).CreatedBy = userName;
                ((IEntity)entity).CreatedAt = currentDate;
                ((IEntity)entity).UpdatedBy = userName;
                ((IEntity)entity).UpdatedAt = currentDate;
            }
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Inserts(_tableName, entitys);
            }
        }
        public IEnumerable<T> AsEnumerable()
        {
            IEnumerable<T> items = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>($"SELECT * FROM {_tableName}");
            }
            return items.AsEnumerable();
        }

        public IQueryable<T> AsQueryable()
        {
            IEnumerable<T> items = null;
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>($"SELECT * FROM {_tableName}");
            }
            return items.AsQueryable();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> items = null;

            // extract the dynamic sql query and parameters from predicate
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>(result.Sql, (object)result.Param);
            }
            return items.Count();
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
            var query = $"UPDATE {_tableName} SET IsDeleted = @IsDeleted,DeletedBy=@DeletedBy,DeletedAt=@DeletedAt,UpdatedBy=@UpdatedBy,UpdatedAt=@UpdatedAt Where Id=@Id";
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(query, entity);
            }
        }
        private string GetUserName()
        {
            try
            {
                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
                return claims?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name, StringComparison.OrdinalIgnoreCase))?.Value;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> items = null;

            // extract the dynamic sql query and parameters from predicate
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                items = cn.Query<T>(result.Sql, (object)result.Param);
            }
            return items;
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return cn.QueryFirst<T>(result.Sql, (object)result.Param);
            }
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return cn.QueryFirstOrDefault<T>(result.Sql, (object)result.Param);
            }
        }

        public T GetById(Guid id)
        {
            T item = default(T);

            using (IDbConnection cn = Connection)
            {
                cn.Open();
                item = cn.Query<T>("SELECT * FROM " + _tableName + " WHERE ID=@ID", new { ID = id }).SingleOrDefault();
            }

            return item;
        }

        public void HardDelete(T entity)
        {
            var query = $"DELETE FROM {_tableName} where Id = @Id";
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Execute(query, entity);
            }
        }

        public T Single(Expression<Func<T, bool>> predicate)
        {
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return cn.QuerySingle<T>(result.Sql, (object)result.Param);
            }
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                return cn.QuerySingleOrDefault<T>(result.Sql, (object)result.Param);
            }
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
            using (IDbConnection cn = Connection)
            {
                var parameters = (object)Mapping(entity);
                cn.Open();
                cn.Update(_tableName, parameters);
            }
        }
        public void Updates(List<T> entitys, bool is_anonymous = false, bool is_time_change = true)
        {
            DateTime currentDate = DateTime.Now;
            foreach (T entity in entitys)
            {
                currentDate = currentDate.AddMilliseconds(10);
                if (!is_anonymous)
                {
                    var userName = GetUserName();
                    ((IEntity)entity).UpdatedBy = userName;
                }

                if (is_time_change)
                    ((IEntity)entity).UpdatedAt = currentDate;
            }
            using (IDbConnection cn = Connection)
            {
                cn.Open();
                cn.Updates(_tableName, entitys);
            }
        }
    }
}
