namespace PandoraWebMvc.Areas.Admin.ViewModels.SliderDiscoveryVideo
{
    public class SliderDiscoveryVideoDetailVM
    {
        public int Id { get; set; }
        public string Video { get; set; }
        public List<SliderDiscoveryVideoTranslateVM> Translates { get; set; }
    }
}
