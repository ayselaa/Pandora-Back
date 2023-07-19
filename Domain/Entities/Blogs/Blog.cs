using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Blogs
{
    public class Blog
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<BlogTranslate> BlogTranslates { get; set; }
    }
}
