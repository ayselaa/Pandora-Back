namespace PandoraWebMvc.Areas.Admin.ViewModels.BannerGift
{
    public class BannerGiftUpdateVM
    {
        public int Id { get; set; }
        public string Photo { get; set; }
        public List<BannerGiftTranslateVM> Translates { get; set; }
    }
}
