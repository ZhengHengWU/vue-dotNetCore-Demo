using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Api.Common;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace TestVueApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigPath.MySqlConnectionStr = Configuration["Connection:MySql"];
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(options => options.AddPolicy("CorsSample", p =>
               p.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
            ));
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v0.1.0",
                    Title = "Api Swagger",
                    Description = "Api说明文档",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Api.Swagger", Email = "2274367387@qq.com", Url = "https://github.com/ZhengHengWU" }
                });
                //如果不加入以下两个xml 也是可以的 但是不会对api有中文说明，使用了一下两个xml 就需要对成员使用///注释
                //本webapi的xml
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Api.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, false);//默认的第二个参数是false，这个是controller的注释，记得修改
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
            #endregion
            //自动配置autofac
            return ConfigureAutofac(services);
            
        }
        /// <summary>
        /// 自动配置autofac
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureAutofac(IServiceCollection services)
        {
            //初始化容器
            var builder = new ContainerBuilder();
            //管道寄居
            builder.Populate(services);
            //业务逻辑层所在程序集命名空间
            Assembly service = Assembly.Load("Api.Service");
            //业务逻辑层接口所在程序集命名空间
            Assembly iService = Assembly.Load("Api.IService");
            //数据访问层所在程序集命名空间
            Assembly data = Assembly.Load("Api.Data");
            //数据访问层接口所在程序集命名空间
            Assembly iData = Assembly.Load("Api.IData");
            //自动注入
            builder.RegisterAssemblyTypes(service, iService, data, iData)
                .AsImplementedInterfaces();
            //构造
            var ApplicationContainer = builder.Build();
            //将AutoFac反馈到管道中
            return new AutofacServiceProvider(ApplicationContainer);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //配置Cors
            app.UseCors("CorsSample");
            //app.UseHttpsRedirection();
            app.UseMvc();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });
            #endregion
        }
    }
}
