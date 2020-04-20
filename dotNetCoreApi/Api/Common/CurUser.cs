using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web.Http;

namespace Md.Api.Common
{
    public static class CurUser
    {
        /// <summary>
        /// 获取当前登录用户的用户编号
        /// </summary>
        public static int CurUserID(this ControllerBase controller)
        {
            var auth = controller.HttpContext.AuthenticateAsync().Result.Principal.Claims;
            var userId = auth.FirstOrDefault(t => t.Type.Equals("Id"))?.Value;

            return userId == null ? 0 : Convert.ToInt32(userId);
        }

        /// <summary>
        /// 获取当前登录用户的姓名
        /// </summary>
        public static string CurUserName(this ControllerBase controller)
        {
            var auth = controller.HttpContext.AuthenticateAsync().Result.Principal.Claims;
            var userName = auth.FirstOrDefault(t => t.Type.Equals("Name"))?.Value;

            return userName == null ? "" : userName;
        }
    }
}
