using Api.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using KeyAttribute = Dapper.KeyAttribute;

namespace Api.Repository
{
    public class BaseRepository<T> : IDisposable where T : class
    {
        static BaseRepository<T> _instance;

        /// <summary>
        ///不允许直接实例化
        /// </summary>
        private BaseRepository()
        {
        }

        /// <summary>
        /// 静态化访问对象
        /// </summary>
        public static BaseRepository<T> Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                
                _instance = new BaseRepository<T>();
                return _instance;
            }
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        public dynamic Insert(T t)
        {
            return DbFactory.Instance.MyConnection.Insert(t, DbFactory.Instance.MyTransaction);
        }

        public TKey Insert<TKey>(T t)
        {
            return DbFactory.Instance.MyConnection.Insert<TKey,T>(t);
        }

        /// <summary>
        /// 批量新增实体
        /// </summary>
        public void Insert(IEnumerable<T> tList)
        {
            DbFactory.Instance.MyConnection.Insert(tList, DbFactory.Instance.MyTransaction);
        }

        /// <summary>
        /// 更新实体：只更新修改字段
        /// </summary>
        public bool Update(T t)
        {
            return DbFactory.Instance.MyConnection.Execute(GetTableUpdateSql(t), t, DbFactory.Instance.MyTransaction) > 0;
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        public void Update(IEnumerable<T> tList)
        {
            //获取更新字符串
            var sql = GetTableUpdateSql(tList.First());

            //批量更新，利用事务
            try
            {
                DbFactory.Instance.BeginTransaction();
                DbFactory.Instance.MyConnection.Execute(sql, tList, DbFactory.Instance.MyTransaction);
                DbFactory.Instance.CommitTransaction();
            }
            catch (Exception)
            {
                DbFactory.Instance.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// 删除实体：此处为真实删除数据库记录
        /// </summary>
        public bool DeleteDbRow(T t)
        {
            return DbFactory.Instance.MyConnection.Delete(t, DbFactory.Instance.MyTransaction) > 0;
        }

        /// <summary>
        /// 删除实体：此处为真实删除数据库记录
        /// </summary>
        public bool DeleteDbRow(object id)
        {
            return DbFactory.Instance.MyConnection.Delete<T>(id, DbFactory.Instance.MyTransaction) > 0;
        }

        /// <summary>
        /// 批量删除实体，此处删除为真实删除数据库记录
        /// </summary>
        public void DeleteDbRow(IEnumerable<T> tList)
        {
            try
            {
                DbFactory.Instance.BeginTransaction();
                foreach (var t in tList)
                {
                    DbFactory.Instance.MyConnection.Delete(t, DbFactory.Instance.MyTransaction);
                }
                DbFactory.Instance.CommitTransaction();
            }
            catch (Exception)
            {
                DbFactory.Instance.RollbackTransaction();
                throw;
            }
        }

        /// <summary>
        /// 查询多个数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="paramValues"></param>
        /// <returns></returns>
        public SqlMapper.GridReader QueryMultiple(string sql, object paramValues = null)
        {
            return DbFactory.Instance.MyConnection.QueryMultiple(sql, paramValues);
        }

        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        public T GetByID<KeyType>(KeyType id)
        {
            return DbFactory.Instance.MyConnection.Get<T>(id);
        }

        /// <summary>
        /// 获取第一个值
        /// </summary>
        public SingleType GetSingle<SingleType>(string sql, object paramValues = null)
        {
            return DbFactory.Instance.MyConnection.QueryFirstOrDefault<SingleType>(sql, paramValues);
        }

        /// <summary>
        /// 执行sql语句返回影响行数
        /// </summary>
        public int Execute(string sql, object paramValues = null)
        {
            return DbFactory.Instance.MyConnection.Execute(sql, paramValues);
        }

        /// <summary>
        /// 查询所有实体
        /// </summary>
        public IEnumerable<T> GetAll()
        {
            return DbFactory.Instance.MyConnection.GetList<T>();
        }

        /// <summary>
        /// 根据Sql查询实体列表
        /// </summary>
        public IEnumerable<T> GetList(string sql, object paramValues = null)
        {
            return DbFactory.Instance.MyConnection.Query<T>(sql, paramValues);
        }

        /// <summary>
        /// 根据Sql查询实体分页列表
        /// </summary>
        public PageList<T> GetPageList(string sql, object paramValues = null, int pageIndex = 1, int pageSize = 10, bool needTotalCount = true)
        {
            if (pageIndex <= 0)
                pageIndex = 1;
            if (pageSize <= 0)
                pageSize = 10;

            var pageList = new PageList<T>() { PageIndex = pageIndex, PageSize = pageSize };
            //获取总数
            if (needTotalCount)
            {
                var sqlTotal = $"select count(0) from ({sql}) t_total";
                var count = DbFactory.Instance.MyConnection.QueryFirstOrDefault<int>(sqlTotal, paramValues);

                pageList.TotalCount = count;
            }
            //获取分页列表
            sql = $"{sql} limit {(pageIndex - 1) * pageSize},{pageSize}";
            var list = DbFactory.Instance.MyConnection.Query<T>(sql, paramValues);

            pageList.AddRange(list);
            return pageList;
        }

        public void Dispose()
        {
            DbFactory.Instance.Dispose();
        }

        /// <summary>
        /// 获取表的更新字符串
        /// </summary>
        private string GetTableUpdateSql(object t)
        {
            //取得m的Type实例
            var mt = t.GetType();

            ////拼接需要更新的属性名
            //var sb = new StringBuilder("EntityState=@EntityState");
            var sb=new StringBuilder();
            //拼接关键字，用于更新的条件
            var where = new StringBuilder();

            //遍历并获取需要更新的属性名
            foreach (var s in mt.GetProperties())
            {
                //如果是创建人和创建日期属性，则不更新
                if (s.Name == "CreateUserID" || s.Name == "CreateDate")
                    continue;

                //过滤Key，获取主键和主键值
                Attribute att = s.GetCustomAttributes().FirstOrDefault(c => c is KeyAttribute || c.GetType() == typeof(AssignedKeyAttribute));
                if (att != null)
                {
                    where.Append($"{s.Name}=@{s.Name} and ");
                    continue;
                }

                //获取属性值
                var ptyValue = s.GetValue(t);
                //如果属性值不为NULL，则更新此属性
                if (ptyValue != null)
                {
                    sb.Append($"{s.Name}=@{s.Name},");
                }
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);

            var sql = $"update {GetTableName(mt)} set {sb} where {where.ToString(0, where.Length - 5)}";
            return sql;
        }

        /// <summary>
        /// 获取实体类特性中设置的表名
        /// </summary>
        private string GetTableName(Type type)
        {
            var tableAttr = type.GetTypeInfo().GetCustomAttributes(false).SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;
            return tableAttr != null ? tableAttr.Name : type.Name;
        }
    }
}
