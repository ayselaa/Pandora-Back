using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Sliders
{
    public class SlidersTranslate
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public string Links { get; set; }
    }
}
