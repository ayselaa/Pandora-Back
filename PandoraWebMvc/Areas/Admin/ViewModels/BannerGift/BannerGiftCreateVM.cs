namespace PandoraWebMvc.Areas.Admin.ViewModels.BannerGift
{
    public class BannerGiftCreateVM
    {
        public int Id { get; set; }
        public IFormFile Photo { get; set; }

        public List<BannerGiftTranslateVM> Translates { get; set; }
    }
}
