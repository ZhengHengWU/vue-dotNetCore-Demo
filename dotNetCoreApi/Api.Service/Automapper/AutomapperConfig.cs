using Api.Entity;
using Api.ViewModels;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service.Automapper
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Blog, BlogView>().ForMember(e => e.Title, v => v.MapFrom(a => a.Title));
        }
    }
}
