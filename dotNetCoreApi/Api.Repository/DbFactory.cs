using Api.Common;
using Dapper;
using System;
using System.Data;

namespace Api.Repository
{
    public class DbFactory
    {
        [ThreadStatic]
        static DbFactory _instance;

        static readonly string connectionString;

        int _transactionCount;

        bool _closeFlag;

        static DbFactory()
        {
            SimpleCRUD.SetDialect(SimpleCRUD.Dialect.MySQL);
            //创建MySql数据库连接对象
            connectionString = ConfigPath.MySqlConnectionStr;
        }

        private DbFactory()
        {
            MyConnection = DbProviderFactories.GetFactory("MySql.Data.MySqlClient").CreateConnection();
            if (MyConnection == null)
                throw new Exception("数据库链接获取失败!");
            MyConnection.ConnectionString = connectionString;
        }

        /// <summary>
        /// mysql数据库的连接对象
        /// </summary>
        public IDbConnection MyConnection { get; }

        /// <summary>
        /// 数据库事务对象
        /// </summary>
        public IDbTransaction MyTransaction { get; private set; }

        /// <summary>
        /// 静态化访问对象
        /// </summary>
        public static DbFactory Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = new DbFactory();
                return _instance;
            }
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        public void BeginTransaction()
        {
            if (_transactionCount == 0)
            {
                if (_closeFlag = (ConnectionState.Closed == MyConnection.State))
                    MyConnection.Open();

                MyTransaction = MyConnection.BeginTransaction();
            }
            _transactionCount++;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void CommitTransaction()
        {
            _transactionCount--;

            if (_transactionCount != 0) return;
            MyTransaction.Commit();
            MyTransaction.Dispose();
            MyTransaction = null;

            if (_closeFlag)
                MyConnection.Close();
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void RollbackTransaction(Exception ex = null)
        {
            if (_transactionCount <= 0)
                return;

            _transactionCount = 0;
            MyTransaction.Rollback();
            MyTransaction.Dispose();
            MyTransaction = null;

            if (_closeFlag)
                MyConnection.Close();

            if (ex != null)
                throw ex;
        }

        public void Dispose()
        {
            if (_closeFlag)
                MyConnection.Close();
            MyConnection.Dispose();
        }
    }
}
