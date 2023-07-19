using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BannerGifts
{
    public class BannerGiftTranslate
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
    }
}
