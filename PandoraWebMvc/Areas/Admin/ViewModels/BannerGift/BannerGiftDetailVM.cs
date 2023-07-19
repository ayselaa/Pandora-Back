namespace PandoraWebMvc.Areas.Admin.ViewModels.BannerGift
{
    public class BannerGiftDetailVM
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public List<BannerGiftTranslateVM> Translates { get; set; }
    }
}
