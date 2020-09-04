using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Internal;
using Renci.SshNet.Messages.Authentication;

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
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public MethodResult Upload([FromForm(Name = "file")] List<IFormFile> files)
        {
            files.ForEach(file =>
            {
                var fileName = file.FileName;
                string fileExtension = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);//获取文件名称后缀 
                //保存文件
                var stream = file.OpenReadStream();
                // 把 Stream 转换成 byte[] 
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始 
                stream.Seek(0, SeekOrigin.Begin);
                // 把 byte[] 写入文件 
                FileStream fs = new FileStream("D:\\" + file.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(bytes);
                bw.Close();
                fs.Close();
            });
            return new MethodResult("success", 1);
        }
    }
}
