using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Api.Common;
using Api.Enum;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using NLog;
using NLog.Web;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            Logger logger = LogManager.GetCurrentClassLogger();
            try
            {
                logger.Trace("网站启动中...");
                using (IServiceScope scope = host.Services.CreateScope())
                {
                    IConfiguration configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                    //获取到appsettings.json中的连接字符串
                    string sqlString = configuration.GetSection("Connection:MySql").Value;
                    //确保NLog.config中连接字符串与appsettings.json中同步
                    NLogUtil.EnsureNlogConfig("NLog.config", sqlString);
                    logger.Trace("初始化数据库");
                }
                //throw new Exception("测试异常");//for test

                //其他项目启动时需要做的事情
                //code
                logger.Trace("网站启动完成");
                host.Run();
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "网站启动失败");
                throw;
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var dic = ReadConfig();
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                }).UseNLog();  // NLog: 依赖注入Nlog;

        }
        private static Dictionary<string, string> ReadConfig()
        {
            try
            {
                using (FileStream fs = new FileStream("httpsConfig.json", FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        return JsonConvert.DeserializeObject<Dictionary<string, string>>(sr.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
