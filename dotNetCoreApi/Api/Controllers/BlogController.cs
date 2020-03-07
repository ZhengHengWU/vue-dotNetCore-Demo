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
    /// <summary>
    /// 博客
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class BlogController : ApiController
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
        public MethodResult GetAllBlogs(int? id = null)
        {
            return new MethodResult(_blogService.GetAllBlogs(id));
        }
        /// <summary>
        /// 通过ID获取博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
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
        public MethodResult DeleteBlogById(int? id = null)
        {
            return new MethodResult(_blogService.DeleteBlogById(id));
        }
    }
}
