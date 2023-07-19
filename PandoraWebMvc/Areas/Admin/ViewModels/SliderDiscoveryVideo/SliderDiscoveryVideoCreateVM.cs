namespace PandoraWebMvc.Areas.Admin.ViewModels.SliderDiscoveryVideo
{
    public class SliderDiscoveryVideoCreateVM
    {
        public IFormFile Photo { get; set; }

        public List<SliderDiscoveryVideoTranslateVM> Translates { get; set; }
    }
}
