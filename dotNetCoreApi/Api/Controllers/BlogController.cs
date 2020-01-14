using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Api.Entity;
using Api.IService;
using Api.Service;
using Md.Api.Common;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace TestVueApi.Controllers
{
    [Route("api/[controller]/[action]")]
    public class BlogController : ApiController
    {
        private readonly IBlogService _blogService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="testService"></param>
        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        public MethodResult GetAllBlogs(int? id = null)
        {
            return new MethodResult(_blogService.GetAllBlogs(id));
        }
        [HttpGet]
        public MethodResult GetBlogById(int? id)
        {
            return new MethodResult(_blogService.GetBlogById(id));
        }
        [HttpPost]
        public MethodResult SaveBlog(Blog blog)
        {
            return new MethodResult(_blogService.SaveBlog(blog));
        }
        [HttpGet]
        public MethodResult DeleteBlogById(int? id = null)
        {
            return new MethodResult(_blogService.DeleteBlogById(id));
        }
    }
}
