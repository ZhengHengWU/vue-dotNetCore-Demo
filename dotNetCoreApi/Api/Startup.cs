using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Api.Common;
using Api.Filter;
using Api.Swagger;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Api
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
            services.Configure<FormOptions>(x => x.MultipartBodyLengthLimit = 1073741822);

            services.AddMvc(opt =>
            {
                opt.Filters.Add(new ProducesAttribute("application/json"));
                opt.Filters.Add<ErrorFilterAttribute>();
            });

            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue; // In case of multipart
            });

            services.Configure<JwtSetting>(Configuration.GetSection("JwtSetting"));
            //HttpResponseMessage结构
            services.AddMvc().AddWebApiConventions();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvcCore().AddApiExplorer();

            //services.AddMvc().AddJsonOptions(options =>
            //{
            //    处理返回结构属性首字母不被小写
            //     对 JSON 数据使用混合大小写。跟属性名同样的大小.输出
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //});
            services.AddCors(options => options.AddPolicy("CorsSample", p =>
               p.WithOrigins("*")
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials()
            ));
            #region Jwt
            var jwtSetting = new JwtSetting();
            Configuration.Bind("JwtSetting", jwtSetting);
            //添加策略鉴权模式
            services.AddAuthorization()
               .AddAuthentication(x =>
               {
                   x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                   x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                   x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
               })
               .AddJwtBearer(option =>
               {
                   option.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateLifetime = true,//是否验证失效时间
                       ClockSkew = TimeSpan.FromSeconds(30),

                       ValidateAudience = true,//是否验证Audience
                                               //ValidAudience = Const.GetValidudience(),//Audience
                                               //这里采用动态验证的方式，在重新登陆时，刷新token，旧token就强制失效了
                       AudienceValidator = (m, n, z) =>
                       {
                           return m != null && m.FirstOrDefault().Equals(jwtSetting.Audience);
                       },
                       ValidateIssuer = true,//是否验证Issuer
                       ValidIssuer = jwtSetting.Issuer,//Issuer，这两项和前面签发jwt的设置一致

                       ValidateIssuerSigningKey = true,//是否验证SecurityKey
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.SecurityKey))//拿到SecurityKey
                   };
               });
            #endregion
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Api Swagger",
                    Description = "Api说明文档",
                    TermsOfService = "None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact { Name = "Api.Swagger", Email = "2274367387@qq.com", Url = "https://github.com/ZhengHengWU" }
                });
                //如果不加入以下两个xml 也是可以的 但是不会对api有中文说明，使用了一下两个xml 就需要对成员使用///注释
                //本webapi的xml
                var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Api.xml");//这个就是刚刚配置的xml文件名
                c.IncludeXmlComments(xmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改
                c.OperationFilter<AddAuthTokenHeaderParameter>();
            });
            #endregion
            //autofac 自动注入
            return ConfigureAutofac(services);
        }
        /// <summary>
        /// autofac 自动注入
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
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true
            });
            //配置Cors
            app.UseCors("CorsSample");
            //app.UseHttpsRedirection();
            app.UseMvc();

            #region Swagger
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Host = httpReq.Host.Value;
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
                c.RoutePrefix = string.Empty;
                c.DefaultModelExpandDepth(1);
                c.DefaultModelRendering(ModelRendering.Example);
                c.DefaultModelsExpandDepth(1);
                c.DisplayOperationId();
                c.DisplayRequestDuration();
                c.DocExpansion(DocExpansion.None);
                c.EnableDeepLinking();
                c.EnableFilter();
                c.MaxDisplayedTags(5);
                c.ShowExtensions();
                c.EnableValidator();
                c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post);
            });
            #endregion
            //绑定设置自定义Json内容
            SiteConfig.SetAppSetting(Configuration.GetSection("JwtSetting"));
        }
    }
}
