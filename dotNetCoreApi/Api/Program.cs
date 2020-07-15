using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var dic = ReadConfig();
            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
            //.ConfigureKestrel(options =>
            //{
            //    options.Listen(IPAddress.Any, Convert.ToInt32(dic["server_port"]), listenOptions =>
            //    {
            //        listenOptions.UseHttps(dic["pfx_name"], dic["pfx_pswd"]);
            //    });
            //})
            //.UseContentRoot(Directory.GetCurrentDirectory())
            //.UseIISIntegration();
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
