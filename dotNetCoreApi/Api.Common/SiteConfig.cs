using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common
{
    /// <summary>
    /// 配置信息读取模型
    /// </summary>
    public static class SiteConfig
    {
        private static IConfigurationSection _appSection = null;

        /// <summary>
        /// API域名地址
        /// </summary>
        public static string AppSetting(string key)
        {
            string str = string.Empty;
            if (_appSection.GetSection(key) != null)
            {
                str = _appSection.GetSection(key).Value;
            }
            return str;
        }

        public static void SetAppSetting(IConfigurationSection section)
        {
            _appSection = section;
        }

        public static string GetSite(string apiName)
        {
            return AppSetting(apiName);
        }
    }
}
