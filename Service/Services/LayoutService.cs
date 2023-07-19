using Domain;
using Domain.Entities.FooterMenus;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LayoutService : ILayoutService
    {
        private readonly AppDbContext _context;
        public LayoutService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FooterMenu>> GetAllAsync()
        {
            return await _context.FooterMenus
                             .Include(f=>f.Translates
                              .Where(ft=>ft.LangCode=="en"))
                              .ToListAsync();
        }

        public async Task<List<FooterItem>> GetAllasync()
        {
            return await _context.FooterItems
                           .Include(f => f.FooterItemTranslates
                            .Where(ft => ft.LangCode == "en"))
                            .Include(f=>f.FooterMenu)
                            .ToListAsync();
        }
    }
}
