using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Banners
{
    public class BannerTranslate
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Links { get; set; }
        public string Image { get; set; }
    }
}
