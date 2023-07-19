using Domain;
using Domain.Entities.Blogs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandoraWebMvc.Areas.Admin.ViewModels.Blog;
using Service.Helpers;

namespace PandoraWebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Blog";
            ViewBag.CurrentAction = "Index";
            List<Blog> blogs = await _context.Blogs
                                     .Include(b => b.BlogTranslates
                                     .Where(bt => bt.LangCode == "az"))
                                     .OrderByDescending(b=>b.Id)
                                     .ToListAsync();
            return View(blogs);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            try
            {
                string filename = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "bloggallery", filename);
                await FileHelper.SaveFileAsync(path, model.Photo);

                Blog newBlog = new Blog()
                {
                    Image = filename
                };

                List<BlogTranslate> blogTranslates = model.Translates.Select(translate => new BlogTranslate
                {
                    LangCode = translate.LangCode,
                    Tittle = translate.Tittle,
                    ShortDescription = translate.ShortDesc,
                    Links = translate.Links
                }).ToList();

                newBlog.BlogTranslates = blogTranslates;

                await _context.Blogs.AddAsync(newBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id is null)
                    return BadRequest();
                var blog = await _context.Blogs.Include(b => b.BlogTranslates).FirstOrDefaultAsync(b => b.Id == id);

                if (blog is null)
                    return NotFound();

                _context.BlogTranslates.RemoveRange(blog.BlogTranslates);
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "bloggallery", blog.Image);
                FileHelper.DeleteFile(path);

                //return RedirectToAction("Index");
                return Ok();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var blog = await _context.Blogs
                           .Include(m => m.BlogTranslates)
                           .FirstOrDefaultAsync(b => b.Id == id);
            if(blog is null) return NotFound();

            BlogUpdateVM model = new BlogUpdateVM()
            {
                Id = blog.Id,
                Image = blog.Image,
                Translates = blog.BlogTranslates.Select(t => new BlogTranslateVM
                {
                    LangCode = t.LangCode,
                    Tittle = t.Tittle,
                    ShortDesc = t.ShortDescription,
                    Links = t.Links
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task <IActionResult> Edit(BlogUpdateVM model, IFormFile NewImage)
        {
            try
            {
                var blog = await _context.Blogs
                                 .Include(b => b.BlogTranslates)
                                 .FirstOrDefaultAsync(b => b.Id == model.Id);

                if (blog is null)
                    return NotFound();

                string imagePath = null;

                if(NewImage != null)
                {
                    if (!string.IsNullOrEmpty(blog.Image))
                    {
                        imagePath = $"{_env.WebRootPath}/bloggallery/{blog.Image}";
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(NewImage.FileName);
                    imagePath = $"{_env.WebRootPath}/bloggallery/{imageName}";
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(stream);
                    }
                    blog.Image = imageName;
                }
                foreach (var translate in model.Translates)
                {
                    var existingTranslate = blog.BlogTranslates
                                                  .FirstOrDefault(t => t.LangCode == translate.LangCode);
                    if (existingTranslate != null)
                    {
                        existingTranslate.Tittle = translate.Tittle;
                        existingTranslate.ShortDescription = translate.ShortDesc;
                        existingTranslate.Links = translate.Links;
                    }
                    else
                    {
                        BlogTranslate newTranslate = new BlogTranslate()
                        {
                            LangCode = translate.LangCode,
                            Tittle = translate.Tittle,
                            ShortDescription = translate.ShortDesc,
                            Links = translate.Links
                        };
                        blog.BlogTranslates.Add(newTranslate);

                    }
                }
                foreach (var existingTranslate in blog.BlogTranslates.ToList())
                {
                    if (!model.Translates.Any(t => t.LangCode == existingTranslate.LangCode))
                        _context.BlogTranslates.Remove(existingTranslate);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task <IActionResult> Detail (int? id)
        {
            if (id is null)
                return BadRequest();

            var blog = await _context.Blogs
                             .Include(m => m.BlogTranslates)
                             .FirstOrDefaultAsync(b => b.Id == id);
            if (blog is null)
                return NotFound();

            BlogDetailVM model = new BlogDetailVM
            {
                Id = blog.Id,
                Image = blog.Image,
                Translates = blog.BlogTranslates.Select(b => new BlogTranslateVM
                {
                    LangCode = b.LangCode,
                    Tittle = b.Tittle,
                    ShortDesc = b.ShortDescription,
                    Links = b.Links
                }).ToList()
            };

            return View(model);
        }
    }
}
