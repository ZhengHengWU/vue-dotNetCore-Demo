using Dapper;
using System;


namespace Api.Entity.Logs
{
    /// <summary>
    /// 错误信息
    /// </summary>
    [Table("app_error")]
    public class ErrorEntity
    {
        /// <summary>
        /// 错误Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// 当前产品版本
        /// </summary>
        public string Version { get; set; } = "";

        /// <summary>
        /// 操作系统信息
        /// </summary>
        public string OsInfo { get; set; } = "";

        /// <summary>
        /// 错误来源
        /// </summary>
        public ErrorFrom ErrorFrom { get; set; } = ErrorFrom.Site;

        /// <summary>
        ///错误发生时正在处理的数据
        /// </summary>
        public string DealData { get; set; } = "";

        /// <summary>
        /// 当前登录用户信息(未登录则为空)
        /// </summary>
        public int UserId { get; set; } = 0;

        /// <summary>
        /// 问题是否已解决
        /// </summary>
        public bool IsSolved { get; set; } = false;

        /// <summary>
        /// 问题解决方法
        /// </summary>
        public string Solution { get; set; } = "";

        /// <summary>
        /// 问题解决人
        /// </summary>
        public string SolvedBy { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public string StackTrace { get; set; } = "";
    }

    /// <summary>
    /// 错误来源
    /// </summary>
    public enum ErrorFrom
    {
        /// <summary>
        /// 网站
        /// </summary>
        Site = 0,
    }
}
