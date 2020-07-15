using Api.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.IService
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllBlogs(int? id);
        Blog GetBlogById(int? id);
        int DeleteBlogById(int? id, int userId);
        int SaveBlog(Blog blog, int userId);
    }
}
