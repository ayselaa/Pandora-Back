using Domain;
using Domain.Entities.Banners;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BannerService : IBannerService
    {
        private readonly AppDbContext _context;
        public BannerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Banner>> GetAllAsync()
        {
            return await _context.Banners.Where(b=>!b.SoftDelete)
                          .Include(b=>b.BannerTranslates
                           .Where(bt=>bt.LangCode == "en"))
                          .ToListAsync();
        }
    }
}
