using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Api.Entity
{
    [Table("Blog")]
    public class Blog: BaseEntity
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public DateTime? date { get; set; }
        public string author { get; set; }
        public string tag { get; set; }
    }
}
