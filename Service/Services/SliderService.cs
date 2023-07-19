using Domain;
using Domain.Entities.Sliders;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;

        public SliderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Slider>> GetAllAsync()
        {
            return await _context.Sliders
                          .Include(s => s.SlidersTranslates
                           .Where(st => st.LangCode == "en"))
                          .ToListAsync();
        }
    }
}
