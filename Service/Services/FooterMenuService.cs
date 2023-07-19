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
    public class FooterMenuService : IFooterMenuService
    {
        private readonly AppDbContext _context;

        public FooterMenuService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FooterMenu>> GetAllAsync()
        {
            return await _context.FooterMenus
               .Include(f => f.Translates
                .Where(st => st.LangCode == "en"))
               .ToListAsync();
        }
    }
}
