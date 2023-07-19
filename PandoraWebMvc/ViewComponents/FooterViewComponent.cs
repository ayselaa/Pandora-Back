using Microsoft.AspNetCore.Mvc;
using PandoraWebMvc.ViewModels.Layout;
using Service.Interfaces;

namespace PandoraWebMvc.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ILayoutService _layoutService;
        private readonly IFooterMenuService _footerMenuService;

        public FooterViewComponent(ILayoutService layoutService, IFooterMenuService footerMenuService)
        {
            _layoutService = layoutService;
            _footerMenuService = footerMenuService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            LayoutVM model = new LayoutVM()
            {
                FooterMenus = await _footerMenuService.GetAllAsync(),
                FooterItems = await _layoutService.GetAllasync()

            };
            return await Task.FromResult(View(model));
        }
    }
}
