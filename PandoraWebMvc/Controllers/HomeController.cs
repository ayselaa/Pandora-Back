using Microsoft.AspNetCore.Mvc;
using PandoraWebMvc.Models;
using PandoraWebMvc.ViewModels.Home;
using Service.Interfaces;
using System.Diagnostics;

namespace PandoraWebMvc.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IBannerService _bannerService;
        private readonly ISliderService _sliderService;
        private readonly ISliderDiscoveryService _sliderDiscoveryService;
        private readonly IBlogService _blogService;
        private readonly IBannerGiftService _bannergiftService;
        private readonly IFooterMenuService _footerMenuService;


        public HomeController(IBannerService bannerService,
                              ISliderService sliderService,
                              ISliderDiscoveryService sliderDiscoveryService,
                              IBlogService blogService, IBannerGiftService bannerGiftService,
                              IFooterMenuService footerMenuService)

        {
            _bannerService = bannerService;
            _sliderService = sliderService;
            _sliderDiscoveryService = sliderDiscoveryService;
            _blogService = blogService;
            _bannergiftService = bannerGiftService;
            _footerMenuService = footerMenuService;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public async Task<IActionResult> Index()
        {
            HomeVM model = new HomeVM()
            {
                Banners = await _bannerService.GetAllAsync(),
                Sliders = await _sliderService.GetAllAsync(),
                SliderDiscoveries = await _sliderDiscoveryService.GetAllAsync(),
                Blogs = await _blogService.GetAllAsync(),
                BannerGifts = await _bannergiftService.GetAllAsync(),
                FooterMenus = await _footerMenuService.GetAllAsync(),
            };
            return View(model);
        }


        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}