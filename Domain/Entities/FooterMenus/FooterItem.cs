using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.FooterMenus
{
    public class FooterItem
    {
        public int Id { get; set; }
        public bool SoftDelete { get; set; } = false;

        public FooterMenu FooterMenu { get; set; }
        public int FooterMenuId { get; set; }

        public List<FooterItemTranslate> FooterItemTranslates { get; set; }
    }
}
