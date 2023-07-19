using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Domain.Entities.Banners
{
    public class Banner
    {
        public int Id { get; set; }
       
        public bool SoftDelete { get; set; } = false;

        public List <BannerTranslate> BannerTranslates { get; set; }
    }
}
