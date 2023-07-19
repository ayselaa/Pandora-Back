using Domain.Entities.BannerGifts;
using Domain.Entities.Banners;
using Domain.Entities.Blogs;
using Domain.Entities.FooterMenus;
using Domain.Entities.SliderDiscovery;
using Domain.Entities.Sliders;

namespace PandoraWebMvc.ViewModels.Home
{
    public class HomeVM
    {
        public List<Banner> Banners { get; set; }
        public List<Slider> Sliders { get; set; }
        public List <SliderDiscovery> SliderDiscoveries { get; set; }
        public List <Blog> Blogs { get; set; }
        public List<BannerGift> BannerGifts { get; set; }
        public List <FooterMenu> FooterMenus { get; set; }
    }
}
