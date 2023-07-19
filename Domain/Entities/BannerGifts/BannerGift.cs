using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.BannerGifts
{
    public class BannerGift
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public List<BannerGiftTranslate> BannerGiftTranslates { get; set; }
    }
}
