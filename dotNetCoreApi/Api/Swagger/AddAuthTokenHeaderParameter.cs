using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Swagger
{
    public class AddAuthTokenHeaderParameter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<IParameter>();
            var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;
            //先判断是否是匿名访问
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                bool isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                var isAuthorized = actionAttributes.Any(a => a is AuthorizeAttribute);
                //非匿名的方法，链接中添加accesstoken值
                if (!isAnonymous && isAuthorized)
                {
                    operation.Parameters.Add(new NonBodyParameter()
                    {
                        Name = "Authorization",
                        In = "header",
                        Type = "string",
                        Default = "",
                        Required = true
                    });
                }
            }
        }
    }
}
