using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common
{
    public class PageList<T> : List<T>, IPageList<T>
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount
        {
            get; set;
        }

        /// <summary>
        /// 第几页
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 一页显示几条数据
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }
    }
}
