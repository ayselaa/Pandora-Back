using Domain.Entities;
using Domain.Entities.BannerGifts;
using Domain.Entities.Banners;
using Domain.Entities.Blogs;
using Domain.Entities.FooterMenus;
using Domain.Entities.Products;
using Domain.Entities.SliderDiscovery;
using Domain.Entities.Sliders;
using Domain.Entities.SlidersDiscoveryVideo;
using Domain.Entities.Stocks;
using Domain.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        //Product
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<ProductStock> ProductStocks { get; set; }
        
        //Slider
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SlidersTranslate> SlidersTranslates { get; set; }

        //Banner
        public DbSet<Banner> Banners { get; set; }
        public DbSet<BannerTranslate> BannerTranslates { get; set; }

        //Slider Video
        public DbSet<SliderDiscoveryVideo> SliderDiscoveryVideos { get; set; }
        public DbSet<SliderDiscoveryVideoTranslate> SliderDiscoveryVideoTranslates { get; set; }

        //Footer Menu
        public DbSet<FooterMenu> FooterMenus { get; set; }
        public DbSet<FooterMenuTranslate> FooterMenuTranslates { get; set; }
        public DbSet<FooterItem> FooterItems { get; set; }
        public DbSet<FooterItemTranslate> FooterItemTranslates { get; set; }

        //Slider Discovery With Photo
        public DbSet<SliderDiscovery> SliderDiscoveries { get; set; }
        public DbSet<SliderDiscoveryTranslate> SliderDiscoveriesTranslates { get; set; }

        //Blog
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogTranslate> BlogTranslates { get; set; }

        //Banner Gift
        public DbSet<BannerGift> BannerGifts { get; set; }
        public DbSet<BannerGiftTranslate> BannerGiftTranslates { get; set; }

    }
}
