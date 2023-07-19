namespace PandoraWebMvc.Areas.Admin.ViewModels.FooterMenuItem
{
    public class FooterMenuItemUpdateVM
    {
        public int Id { get; set; }
        public int FooterMenuId { get; set; }
        public List<FooterMenuItemTranslateVM> Translates { get; set; }
    }
}
