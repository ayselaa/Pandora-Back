using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FooterMenus
{
    public class FooterMenu
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; } = false;


        public List<FooterMenuTranslate> Translates { get; set; }
        public List<FooterItem> Items { get; set; }


    }
}
