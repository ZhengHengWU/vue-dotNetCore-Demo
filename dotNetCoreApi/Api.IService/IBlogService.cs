using Api.Entity;
using System;
using System.Collections.Generic;

namespace Api.IService
{
    public interface IBlogService
    {
        List<Blog> GetAllBlogs(int? id);
        Blog GetBlogById(int? id);
        int DeleteBlogById(int? id);
        int SaveBlog(Blog blog);
    }
}
