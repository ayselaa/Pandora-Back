using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.SlidersDiscoveryVideo
{
    public class SliderDiscoveryVideo
    {
        public int Id { get; set; }
        public string Video { get; set; }

        public List<SliderDiscoveryVideoTranslate> SliderDiscoveryVideoTranslates { get; set; }

    }
}
