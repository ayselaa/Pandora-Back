namespace PandoraWebMvc.Areas.Admin.ViewModels.SliderDiscovery
{
    public class SliderDiscoveryDetailVM
    {
        public int Id { get; set; }
        public string Photo { get; set; }

        public List<SliderDiscoveryTranslateVM> Translates { get; set; }
    }
}
