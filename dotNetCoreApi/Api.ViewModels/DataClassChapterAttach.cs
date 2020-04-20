using System;
using System.Collections.Generic;
using System.Text;

namespace Api.ViewModels
{
    public class DataClassChapter
    {
        public int Id { get; set; }
        public List<DataClassChapterAttach> Attach { get; set; }
    }
    public class DataClassChapterAttach
    {
        public int ID { get; set; }

        public string FilePath { get; set; }

        public int FileType { get; set; }

        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FileExt { get; set; }

        public int FileSize { get; set; }
    }
}
