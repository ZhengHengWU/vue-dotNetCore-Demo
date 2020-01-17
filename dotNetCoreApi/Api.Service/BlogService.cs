using Api.Data;
using Api.Entity;
using Api.IData;
using Api.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service
{
    public class BlogService : IBlogService
    {
        private readonly IBlogData _blogData;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="blogData"></param>
        public BlogService(IBlogData blogData)
        {
            _blogData = blogData;
        }
        public List<Blog> GetAllBlogs(int? id)
        {
            return _blogData.GetAllBlogs(id);
        }

        public Blog GetBlogById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                var blog = _blogData.GetBlogById(Convert.ToInt32(id));
                return blog;
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="blog"></param>
        /// <returns></returns>
        public int SaveBlog(Blog blog)
        {
            return _blogData.SaveBlog(blog);
        }
        public int DeleteBlogById(int? id)
        {
            if (id == null)
            {
                return 0;
            }
            else
            {
                return _blogData.DeleteBlogById(id ?? 0);
            }
        }
    }
}
