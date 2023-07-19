namespace PandoraWebMvc.Areas.Admin.ViewModels.SliderDiscovery
{
    public class SliderDiscoveryCreateVM
    {
        public IFormFile Photo { get; set; }

        public List<SliderDiscoveryTranslateVM> Translates { get; set; }
    }
}
