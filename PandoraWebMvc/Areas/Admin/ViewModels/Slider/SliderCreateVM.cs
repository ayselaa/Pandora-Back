using System.ComponentModel.DataAnnotations;

namespace PandoraWebMvc.Areas.Admin.ViewModels.Slider
{
    public class SliderCreateVM
    {
        //[Required(ErrorMessage = "Don`t be empty")]
        public IFormFile Photo { get; set; }

        public List<SliderTranslateVM> Translates { get; set; }

    }
}
