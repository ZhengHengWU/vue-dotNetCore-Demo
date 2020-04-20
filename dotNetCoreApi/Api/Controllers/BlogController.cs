using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Entity;
using Api.IService;
using Api.Service;
using Api.ViewModels;
using Md.Api.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TestVueApi.Controllers
{
    /// <summary>
    /// 博客
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="blogService"></param>
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public MethodResult GetAllBlogs(int? id = null)
        {
            var userId = this.CurUserID();
            return new MethodResult(_blogService.GetAllBlogs(id));
        }
        /// <summary>
        /// 通过ID获取博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public MethodResult GetBlogById(int? id)
        {


            return new MethodResult(_blogService.GetBlogById(id));
        }
        /// <summary>
        /// 保存博客信息
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public MethodResult SaveBlog(Blog blog)
        {
            return new MethodResult(_blogService.SaveBlog(blog));
        }
        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public MethodResult DeleteBlogById(int? id = null)
        {
            return new MethodResult(_blogService.DeleteBlogById(id));
        }
        /// <summary>
        /// pdf转化为图片
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public MethodResult PdfConvertJpg(DataClassChapter model)
        {
            var pdfList = model.Attach.ToList();
            //pdf转化为图片
            foreach (var item in pdfList)
            {
                //保存pdf信息到附件表
                var device = new Aspose.Pdf.Devices.JpegDevice(80);
                var config = "D:\\Web\\Md.Api.Down\\";
                var path = config + item.FilePath;
                //pdf转为图片
                using (var document = new Aspose.Pdf.Document(path))
                {
                    for (var t = 1; t <= document.Pages.Count; t++)
                    {
                        //图片名称
                        var newName = Guid.NewGuid().ToString("N");

                        //保存图片
                        var lastIndex = item.FilePath.LastIndexOf('\\');
                        var newPath = item.FilePath.Substring(0, lastIndex + 1);
                        var filePath = config + newPath + newName + ".jpg";
                        var temp_path = newPath + newName + ".jpg";
                        var fileInfo = new FileInfo(filePath);
                        using (var fs = new FileStream(filePath, FileMode.OpenOrCreate))
                        {
                            device.Process(document.Pages[t], fs);
                            fs.Close();
                        }
                    }
                }
            }
            return new MethodResult("Success");
        }
    }
}
