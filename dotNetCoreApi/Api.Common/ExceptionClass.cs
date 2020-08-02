using System;

namespace Api.Common
{
    /// <summary>
    /// 异常帮助类
    /// </summary>
    public class ExceptionClass
    {
        /// <summary>
        /// 抛出异常方法
        /// </summary>
        public static Exception Throw(Exception ex)
        {
            return new Exception(ex.Message, ex);
        }
    }
}
