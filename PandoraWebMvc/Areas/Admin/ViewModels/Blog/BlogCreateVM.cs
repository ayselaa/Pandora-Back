namespace PandoraWebMvc.Areas.Admin.ViewModels.Blog
{
    public class BlogCreateVM
    {
        public IFormFile Photo { get; set; }
        public List<BlogTranslateVM> Translates { get; set; }
    }
}
