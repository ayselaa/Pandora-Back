using Domain;
using Domain.Entities.SliderDiscovery;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SliderDiscoveryService : ISliderDiscoveryService
    {
        private readonly AppDbContext _context;
        public SliderDiscoveryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<SliderDiscovery>> GetAllAsync()
        {
            return await _context.SliderDiscoveries
                         .Include(s => s.SliderDiscoveryTranslates
                         .Where(st => st.LangCode == "en"))
                         .ToListAsync();


        }
    }
}
