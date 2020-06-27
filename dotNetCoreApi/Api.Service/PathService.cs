using Api.Common;
using Api.Data;
using Api.Entity;
using Api.IData;
using Api.IService;
using Api.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Service
{
    public class PathService : IPathService
    {
        IOptionsSnapshot<SystemPath> _options;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="blogData"></param>
        public PathService(IOptionsSnapshot<SystemPath> options)
        {
            _options = options;
        }
        public string GetPath()
        {
            return _options.Value.FilePath;
        }
    }
}
