using Api.Entity;
using Api.IData;
using Api.Service;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Data
{
    public class BlogData : IBlogData
    {
        public List<Blog> GetAllBlogs(int? id)
        {
            string sql = "select * from blog where EntityState<2 ";
            if (id != null)
            {
                sql += " and Id=@id";
            }

            return DbData.GetList<Blog>(sql, new { id })?.ToList();
        }
        public int SaveBlog(Blog blog)
        {
            if (blog.id > 0)
            {
                DbData.Update<Blog>(blog, 204);
                return blog.id;
            }
            return DbData.Insert<Blog>(blog, 204);
        }
        public Blog GetBlogById(int id)
        {
            return DbData.GetByID<Blog>(id);
        }
        public int DeleteBlogById(int id)
        {
            return DbData.Delete<Blog>(new Blog { id = id }, 204) == true ? id : 0;
        }
    }
}
