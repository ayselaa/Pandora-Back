using Domain;
using Domain.Entities.Blogs;
using Microsoft.EntityFrameworkCore;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;

        public BlogService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Blog>> GetAllAsync()
        {
            return await _context.Blogs
                         .Include(b => b.BlogTranslates
                         .Where(bt => bt.LangCode == "en"))
                          .OrderByDescending(b => b.CreatedAt)
                          .Take(3)
                         .ToListAsync();
        }
    }
}
