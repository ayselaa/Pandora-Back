namespace PandoraWebMvc.Areas.Admin.ViewModels.Blog
{
    public class BlogUpdateVM
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public List<BlogTranslateVM> Translates { get; set; }
    }
}
