using Api.Entity.Logs;
using Api.Service;
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
            #region  如果是NoLogException则不计入日志
            var exType = context.Exception.GetType();
            if (exType.Name == "NoLogException" || exType.Name == "OperationCanceledException" || context.ActionDescriptor.DisplayName.Contains("Api.Controllers.AuthController.GetToken"))
            {
                result.Content = JsonConvert.SerializeObject(new MethodResult(context.Exception.Message));
                context.Result = result;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.ExceptionHandled = true;
                return;
            }
            #endregion

            var error = new ErrorEntity
            {
                CreateDate = DateTime.Now,
                DealData = "",
                ErrorFrom = ErrorFrom.Site,
                Exception = context.Exception.ToString(),
                IsSolved = false,
                OsInfo = Environment.OSVersion.Platform.ToString(),
                Solution = "",
                SolvedBy = "",
                UserId = context.HttpContext.AuthenticateAsync().Result.Principal.CurUserID(),
                Version = "1.0.0",
                StackTrace = context.Exception.StackTrace
            };
            var thread = new Thread(AddError);
            thread.Start(error);

            result.Content = JsonConvert.SerializeObject(new MethodResult(context.Exception.Message));
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
