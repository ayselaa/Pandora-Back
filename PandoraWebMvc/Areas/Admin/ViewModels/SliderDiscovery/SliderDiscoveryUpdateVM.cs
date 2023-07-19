namespace PandoraWebMvc.Areas.Admin.ViewModels.SliderDiscovery
{
    public class SliderDiscoveryUpdateVM
    {
        public int Id { get; set; }
        public string Photo { get; set; }

        public List<SliderDiscoveryTranslateVM> Translates { get; set; }
    }
}
