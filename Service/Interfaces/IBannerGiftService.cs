using Domain.Entities.BannerGifts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBannerGiftService
    {
        Task<List<BannerGift>> GetAllAsync();
    }
}
