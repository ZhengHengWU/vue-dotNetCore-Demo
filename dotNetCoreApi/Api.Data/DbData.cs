using Api.Common;
using Api.Entity;
using Api.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace Api.Service
{
   static class DbData
    {
        /// <summary>
        /// 开启事务
        /// </summary>
        public static void BeginTransaction()
        {
            DbFactory.Instance.BeginTransaction();
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public static void CommitTransaction()
        {
            DbFactory.Instance.CommitTransaction();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public static void RollbackTransaction(Exception ex)
        {
            DbFactory.Instance.RollbackTransaction(ex);
        }

        public static void Insert<T>(IEnumerable<T> tList) where T : class
        {
            BaseRepository<T>.Instance.Insert(tList);
        }

        public static dynamic Insert<T>(T t, int userId) where T : BaseEntity
        {
            t.CreateUserID = userId;
            t.CreateDate = DateTime.Now;

            t.EntityState = EntityState.Add;

            t.LastModifyUserID = userId;
            t.LastModifyDate = DateTime.Now;

            return BaseRepository<T>.Instance.Insert(t);
        }

        public static TKey Insert<T, TKey>(T t, int userId) where T : BaseEntity
        {
            t.CreateUserID = userId;
            t.CreateDate = DateTime.Now;

            t.EntityState = EntityState.Add;

            t.LastModifyUserID = userId;
            t.LastModifyDate = DateTime.Now;

            return BaseRepository<T>.Instance.Insert<TKey>(t);
        }

        public static void Insert<T>(IEnumerable<T> tList, int userId) where T : BaseEntity
        {
            foreach (var t in tList)
            {
                t.CreateUserID = userId;
                t.CreateDate = DateTime.Now;

                t.EntityState = EntityState.Add;

                t.LastModifyUserID = userId;
                t.LastModifyDate = DateTime.Now;
            }
            BaseRepository<T>.Instance.Insert(tList);
        }

        public static bool Update<T>(T t, int userId) where T : BaseEntity
        {
            t.LastModifyDate = DateTime.Now;
            t.LastModifyUserID = userId;
            t.EntityState = EntityState.Modify;

            return BaseRepository<T>.Instance.Update(t);
        }

        public static void Update<T>(IEnumerable<T> tList, int userId) where T : BaseEntity
        {
            foreach (var t in tList)
            {
                t.LastModifyDate = DateTime.Now;
                t.LastModifyUserID = userId;
                t.EntityState = EntityState.Modify;
            }
            BaseRepository<T>.Instance.Update(tList);
        }

        public static T GetByID<T>(int id) where T : BaseEntity
        {
            return BaseRepository<T>.Instance.GetByID(id);
        }

        public static T GetByID<T>(string id) where T : BaseEntity
        {
            return BaseRepository<T>.Instance.GetByID(id);
        }

        /// <summary>
        /// 获取第一个值
        /// </summary>
        public static SingleType GetSingle<SingleType>(string sql, object paramValues = null)
        {
            return BaseRepository<dynamic>.Instance.GetSingle<SingleType>(sql, paramValues);
        }

        /// <summary>
        /// 假删
        /// </summary>
        public static bool Delete<T>(T t, int userId) where T : BaseEntity
        {
            t.LastModifyDate = DateTime.Now;
            t.LastModifyUserID = userId;
            t.EntityState = EntityState.Delete;

            return BaseRepository<T>.Instance.Update(t);
        }

        /// <summary>
        /// 假删
        /// </summary>
        public static void Delete<T>(IEnumerable<T> tList, int userId) where T : BaseEntity
        {
            foreach (var t in tList)
            {
                t.LastModifyDate = DateTime.Now;
                t.LastModifyUserID = userId;
                t.EntityState = EntityState.Delete;
            }
            BaseRepository<T>.Instance.Update(tList);
        }

        /// <summary>
        /// 真实删除
        /// </summary>
        public static bool DeleteDbRow<T>(T t) where T : BaseEntity
        {
            return BaseRepository<T>.Instance.DeleteDbRow(t);
        }

        /// <summary>
        /// 真实删除
        /// </summary>
        public static bool DeleteDbRow<T>(object id) where T : BaseEntity
        {
            return BaseRepository<T>.Instance.DeleteDbRow(id);
        }

        /// <summary>
        /// 真实删除
        /// </summary>
        public static void DeleteDbRow<T>(IEnumerable<T> tList) where T : BaseEntity
        {
            BaseRepository<T>.Instance.DeleteDbRow(tList);
        }

        public static IEnumerable<T> GetAll<T>() where T : BaseEntity
        {
            return BaseRepository<T>.Instance.GetAll();
        }

        public static IEnumerable<T> GetList<T>(string sql, object paramValues = null) where T : class
        {
            return BaseRepository<T>.Instance.GetList(sql, paramValues);
        }

        public static PageList<T> GetPageList<T>(string sql, object paramValues = null, int pageIndex = 1, int pageSize = 10, bool needTotalCount = true) where T : class
        {
            return BaseRepository<T>.Instance.GetPageList(sql, paramValues, pageIndex, pageSize, needTotalCount);
        }

        /// <summary>
        /// 执行sql 返回影响行数（非万不得已，不准使用该方法）
        /// </summary>
        public static int Execute<T>(string sql, object paramValues = null) where T : class
        {
            return BaseRepository<T>.Instance.Execute(sql, paramValues);
        }

        /// <summary>
        /// 针对不继承BaseEntity的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static dynamic Insert<T>(T t) where T : class
        {
            return BaseRepository<T>.Instance.Insert(t);
        }

        /// <summary>
        /// 针对不继承BaseEntity的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static dynamic Update<T>(T t) where T : class
        {
            return BaseRepository<T>.Instance.Update(t);
        }

        /// <summary>
        /// DataTable转换成集合
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="dataSet">数据源</param>
        /// <param name="tableIndex">需要转换表的索引</param>
        /// <returns></returns>
        public static IList<T> DataTableToList<T>(DataTable dataTable)
        {
            //确认参数有效
            if (dataTable == null || dataTable.Rows.Count == 0 || dataTable.Columns.Count == 0)
            {
                return null;
            }
            DataTable dt = dataTable;

            IList<T> list = new List<T>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //创建泛型对象
                T _t = Activator.CreateInstance<T>();
                //获取对象所有属性
                PropertyInfo[] propertyInfo = _t.GetType().GetProperties();
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    foreach (PropertyInfo info in propertyInfo)
                    {
                        //属性名称和列名相同时赋值
                        if (dt.Columns[j].ColumnName.ToUpper().Equals(info.Name.ToUpper()))
                        {
                            if (dt.Rows[i][j] != DBNull.Value)
                            {
                                info.SetValue(_t, dt.Rows[i][j], null);
                            }
                            else
                            {
                                info.SetValue(_t, null, null);
                            }
                            break;
                        }
                    }
                }
                list.Add(_t);
            }
            return list;
        }
    }
}
