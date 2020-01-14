using System;
using System.Collections.Generic;
using System.Text;

namespace Md.Api.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class MethodResult
    {
        /// <summary>
        /// 
        /// </summary>
        public MethodResult()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="state">1正常  0业务异常   2数据删除   3没有权限</param>
        public MethodResult(dynamic data, int state = 1)
        {
            State = state;
            Data = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <param name="state">1正常  0业务异常   2数据删除   3没有权限</param>
        public MethodResult(string input, int state = 0)
        {
            State = state;

            if (state <= 0)
            {
                ErrorMessage = input;
            }
            else
            {
                Data = input;
            }
        }

        /// <summary>
        /// 返回值状态，小于等于0失败，大于等于1成功
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 返回值内容
        /// </summary>
        public dynamic Data { get; set; }

        /// <summary>
        /// 错误描述
        /// </summary>
        public string ErrorMessage { get; set; }


    }
}
