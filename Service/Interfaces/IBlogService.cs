using Domain.Entities.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IBlogService
    {
        Task<List<Blog>> GetAllAsync();
    }
}
