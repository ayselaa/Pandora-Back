using Domain.Entities.SliderDiscovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISliderDiscoveryService
    {
        Task<List<SliderDiscovery>> GetAllAsync();
    }
}
