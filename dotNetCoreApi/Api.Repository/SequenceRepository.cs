using System;
using System.Configuration;
using Api.Common;
using Api.Entity.Common;
using Dapper;


namespace Api.Repository
{
    public class SequenceRepository
    {
        /// <summary>
        /// 生成序列号
        /// </summary>
        /// <param name="userId">当前登录人</param>
        /// <returns></returns>
        public static string CreateSequenceNumber(int userId)
        {
            using (var conn = DbProviderFactories.GetFactory("MySql.Data.MySqlClient").CreateConnection())
            {
                conn.ConnectionString = ConfigPath.MySqlConnectionStr;
                //conn.Open();
                var pad = 4; //补位数
                var currentDate = DateTime.Now.ToString("yyMMdd");
                const string sql = "select * from Sequence";
                var sequenceInfo = conn.QueryFirstOrDefault<SequenceEntity>(sql);
                if (sequenceInfo != null)
                {
                    //前缀的日期字符串相等，直接加1
                    if (currentDate == sequenceInfo.DateNumber)
                    {
                        sequenceInfo.Number = sequenceInfo.Number + 1;
                    }
                    else
                    {
                        //小时不相等，从1开始重新计算
                        sequenceInfo.DateNumber = currentDate;
                        sequenceInfo.Number = 1;
                    }
                    sequenceInfo.LastModifyDate = DateTime.Now;
                    sequenceInfo.LastModifyUserID = userId;
                    string update = $"update Sequence set DateNumber='{currentDate}',Number={sequenceInfo.Number},LastModifyUserID={userId},LastModifyDate='{DateTime.Now}' where Id={sequenceInfo?.ID}";
                    conn.Execute(update);
                }
                else
                {
                    sequenceInfo = new SequenceEntity
                    {
                        DateNumber = currentDate,
                        Number = 1,
                        CreateUserID = userId,
                        CreateDate = DateTime.Now,
                        EntityState = 0,
                        LastModifyDate = DateTime.Now,
                        LastModifyUserID = userId
                    };
                    string insert = $"insert into Sequence(DateNumber,Number,CreateUserID,CreateDate,LastModifyUserID,LastModifyDate,EntityState) values('{sequenceInfo.DateNumber}'," +
                    $"{sequenceInfo.Number},{sequenceInfo.CreateUserID},'{sequenceInfo.CreateDate}',{sequenceInfo.LastModifyUserID},'{sequenceInfo.LastModifyDate}',{(int)sequenceInfo.EntityState})";
                    conn.Execute(insert);
                }
                conn.Close();
                return sequenceInfo.DateNumber + sequenceInfo.Number.ToString().PadLeft(pad, '0');
            }
        }

    }
}
