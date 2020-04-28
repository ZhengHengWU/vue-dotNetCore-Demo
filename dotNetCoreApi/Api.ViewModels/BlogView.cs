using System;

namespace Api.ViewModels
{
    public class BlogView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public DateTime? Date { get; set; }
        public string Author { get; set; }
        public string Tag { get; set; }
    }
}
