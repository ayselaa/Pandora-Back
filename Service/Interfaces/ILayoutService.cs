using Domain.Entities.FooterMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ILayoutService
    {
        Task<List<FooterMenu>> GetAllAsync();
        Task<List<FooterItem>> GetAllasync();
    }
}
