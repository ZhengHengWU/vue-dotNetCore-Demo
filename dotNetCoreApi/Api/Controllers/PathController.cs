using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Common;
using Api.Entity;
using Api.IService;
using Api.Service;
using Api.ViewModels;
using Md.Api.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    /// <summary>
    /// 路径
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PathController : ControllerBase
    {
        private readonly IPathService _pathService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pathService"></param>
        public PathController(IPathService pathService)
        {
            _pathService = pathService;
        }
        /// <summary>
        /// 获取系统路径
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public MethodResult GetSystemPath()
        {
            var path = GlobalConfig.systemPath.FilePath;
            return new MethodResult(path, 1);
        }
    }
}
