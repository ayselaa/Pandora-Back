using Domain;
using Domain.Entities.BannerGifts;
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
    public class BannerGiftService : IBannerGiftService
    {
        private readonly AppDbContext _context;
        public BannerGiftService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BannerGift>> GetAllAsync()
        {
            return await _context.BannerGifts
                              .Include(b=>b.BannerGiftTranslates
                              .Where(b=>b.LangCode == "en"))
                              .ToListAsync();
        }
       
    }
}
