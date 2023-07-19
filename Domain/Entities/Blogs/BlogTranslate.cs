using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Blogs
{
    public class BlogTranslate
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Tittle { get; set; }
        public string ShortDescription { get; set; }
        public string Links { get; set; }
    }
}
