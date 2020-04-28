﻿using Api.Entity;
using System;
using System.Collections.Generic;

namespace Api.IData
{
    public interface IBlogData
    {
        List<Blog> GetAllBlogs(int? id);
        int SaveBlog(Blog blog, int userId);
        Blog GetBlogById(int id);
        int DeleteBlogById(int id, int userId);
    }
}
