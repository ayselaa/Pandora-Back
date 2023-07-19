using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.SliderDiscovery
{
    public class SliderDiscovery
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public List<SliderDiscoveryTranslate> SliderDiscoveryTranslates { get; set; }

    }
}
