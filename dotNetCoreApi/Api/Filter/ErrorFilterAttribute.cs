using Api.Entity.Logs;
using Api.Service;
using Api.Service.Common;
using Md.Api.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading;

namespace Api.Filter
{
    /// <summary>
    /// 异常处理
    /// </summary>
    public class ErrorFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 处理异常信息
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            ContentResult result = new ContentResult
            {
                StatusCode = 500,
                ContentType = "application/json; charset=utf-8"
            };
            var userId = context.HttpContext.AuthenticateAsync().Result.Principal.CurUserID();
            //获取异常代码
            var errorCode = Generator.CreateErrorCode(userId);
            var error = new ErrorEntity
            {
                CreateDate = DateTime.Now,
                DealData = "",
                ErrorFrom = ErrorFrom.Site,
                ErrorCode = errorCode,
                Exception = context.Exception.ToString(),
                IsSolved = false,
                OsInfo = Environment.OSVersion.Platform.ToString(),
                Solution = "",
                SolvedBy = "",
                UserId = userId,
                Version = "1.0.0",
                StackTrace = context.Exception.StackTrace
            };
            var thread = new Thread(AddError);
            thread.Start(error);

            result.Content = JsonConvert.SerializeObject(new MethodResult(context.Exception.Message, 0, errorCode));
            context.Result = result;
            context.ExceptionHandled = true;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        /// <summary>
        /// 异步保存异常信息
        /// </summary>
        /// <param name="obj"></param>
        public void AddError(object obj)
        {
            var error = obj as ErrorEntity;
            if (error == null) return;
            var service = new ErrorService();
            service.AddError(error);
        }
    }
}
