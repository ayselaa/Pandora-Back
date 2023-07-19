namespace PandoraWebMvc.Areas.Admin.ViewModels.FooterMenuItem
{
    public class FooterMenuItemVM
    {
        public int Id { get; set; }
        public string FooterMenuName { get; set; }
        public List<FooterMenuItemTranslateVM> Translate { get; set; }
    }
}
