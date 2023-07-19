namespace PandoraWebMvc.Areas.Admin.ViewModels.Slider
{
    public class SliderUpdateVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public List <SliderTranslateVM> Translates { get; set; }
    }
}
