using Api.Data;
using Api.Entity;
using Api.IData;
using Api.IService;
using Api.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service
{
    public class BlogService : IBlogService
    {
        private readonly IBlogData _blogData;
        private IMapper _mapper;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="blogData"></param>
        public BlogService(IBlogData blogData, IMapper mapper)
        {
            _blogData = blogData;
            _mapper = mapper;
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
        public int SaveBlog(Blog blog, int userId)
        {
            BlogView model = new BlogView();
            _mapper.Map(blog, model);
            return _blogData.SaveBlog(blog, userId);
        }
        public int DeleteBlogById(int? id, int userId)
        {
            if (id == null)
            {
                return 0;
            }
            else
            {
                return _blogData.DeleteBlogById(id ?? 0, userId);
            }
        }
    }
}
