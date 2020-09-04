using Api.Common;
using Api.Entity;
using Api.IData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Data
{
    public class BlogData : IBlogData
    {
        public async Task<List<Blog>> GetAllBlogs(int? id)
        {
            try
            {
                string sql = "select * from blog where EntityState<2 ";
                if (id != null)
                {
                    sql += " and Id=@id";
                }

                return DbData.GetList<Blog>(sql, new { id })?.ToList();
            }
            catch (Exception ex)
            {
                throw ExceptionClass.Throw(ex);
            }

        }
        public int SaveBlog(Blog blog, int userId)
        {
            if (blog.Id > 0)
            {
                DbData.Update<Blog>(blog, userId);
                return blog.Id;
            }
            return DbData.Insert<Blog>(blog, userId);
        }
        public Blog GetBlogById(int id)
        {
            return DbData.GetByID<Blog>(id);
        }
        public int DeleteBlogById(int id, int userId)
        {
            return DbData.Delete<Blog>(new Blog { Id = id }, userId) == true ? id : 0;
        }
    }
}
