using Microsoft.Extensions.DependencyInjection;
using Service.Helpers;
using Service.Interfaces;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IBannerService, BannerService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ISliderDiscoveryService, SliderDiscoveryService>();
            services.AddScoped<IBlogService,BlogService>();
            services.AddScoped<IBannerGiftService, BannerGiftService>();
            services.AddScoped<IFooterMenuService, FooterMenuService>();
            services.AddScoped<ILayoutService, LayoutService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<EmailSettings>();




            return services;

        }
    }
}
