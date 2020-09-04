using System;
using System.ComponentModel;

namespace Api.Enum
{
    public enum LogType
    {
        [Description("网站")]
        Web,
        [Description("数据库")]
        DataBase,
        [Description("Api接口")]
        ApiRequest,
        [Description("中间件")]
        Middleware
    }
}
