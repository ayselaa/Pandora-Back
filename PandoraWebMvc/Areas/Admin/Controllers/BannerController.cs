using Domain;
using Domain.Entities.Banners;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandoraWebMvc.Areas.Admin.ViewModels;
using PandoraWebMvc.Areas.Admin.ViewModels.Banner;
using Service.Helpers;

namespace PandoraWebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class BannerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BannerController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Banner";
            ViewBag.CurrentAction = "Index";
            List<Banner> banners = await _context.Banners
                                         //.Where(b=> !b.SoftDelete)
                                         .Include(b => b.BannerTranslates)
                                         .OrderByDescending(b=>b.Id)
                                         .ToListAsync();
            return View(banners);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(BannerCreateVM model)
        {
            try
            {
                List<BannerTranslate> bannerTranslates = new List<BannerTranslate>();

                for (int i = 0; i < model.Translates.Count; i++)
                {
                    string filename = Guid.NewGuid().ToString() + "_" + model.Translates[i].Photo.FileName;
                    string path = FileHelper.GetFilePath(_env.WebRootPath, "bannergallery", filename);
                    await FileHelper.SaveFileAsync(path, model.Translates[i].Photo);

                    BannerTranslate translate = new BannerTranslate
                    {
                        LangCode = model.Translates[i].LangCode,
                        Links = model.Translates[i].Links,
                        Image = filename
                    };

                    bannerTranslates.Add(translate);
                }

                Banner newBanner = new Banner
                {
                    BannerTranslates = bannerTranslates
                };

                await _context.Banners.AddAsync(newBanner);
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

                var banner = await _context.Banners
                    .Where(b => !b.SoftDelete)
                    .Include(b => b.BannerTranslates)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (banner is null)
                    return NotFound();

                if (banner.BannerTranslates != null)
                {
                    foreach (var translate in banner.BannerTranslates)
                    {

                        string filePath = Path.Combine(_env.WebRootPath, "bannergallery", translate.Image);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                    }

                    _context.BannerTranslates.RemoveRange(banner.BannerTranslates);
                    _context.Banners.Remove(banner);
                    await _context.SaveChangesAsync();
                }

                //return RedirectToAction("Index");
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SetStatus(int? id)
        {
            if (id is null)
                return BadRequest();

            var banner = await _context.Banners
                               //.Include(b => !b.SoftDelete)
                               .FirstOrDefaultAsync(b => b.Id == id);
            if (banner is null)
                return NotFound();

            banner.SoftDelete = !banner.SoftDelete;
            await _context.SaveChangesAsync();

            return Ok(banner.SoftDelete);

        }

        [HttpGet]

        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
                return BadRequest();
            var banner = await _context.Banners
                               .Where(b => !b.SoftDelete)
                               .Include(b => b.BannerTranslates)
                               .FirstOrDefaultAsync(b => b.Id == id);
            if (banner is null)
                return NotFound();

            BannerDetailVM model = new BannerDetailVM
            {
                Id = banner.Id,
                Translates = banner.BannerTranslates.Select(t => new BannerTranslateVM
                {
                    LangCode = t.LangCode,
                    Links = t.Links,
                    Image = t.Image

                }).ToList()
            };
            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var banner = await _context.Banners
                .Include(b => b.BannerTranslates)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (banner is null)
                return NotFound();

            BannerUpdateVM model = new BannerUpdateVM
            {
                Id = banner.Id,
                Translates = banner.BannerTranslates.Select(t => new BannerTranslateVM
                {
                    LangCode = t.LangCode,
                    Photo = null,
                    Image = t.Image,
                    Links = t.Links
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(BannerUpdateVM model)
        {
            try
            {

                var banner = await _context.Banners
                    .Include(b => b.BannerTranslates)
                    .FirstOrDefaultAsync(b => b.Id == model.Id);

                if (banner is null)
                    return NotFound();


                for (int i = 0; i < model.Translates.Count; i++)
                {
                    var translate = banner.BannerTranslates.ElementAtOrDefault(i);
                    if (translate != null)
                    {
                        translate.LangCode = model.Translates[i].LangCode;
                        translate.Links = model.Translates[i].Links;

                        var uploadedPhoto = model.Translates[i].Photo;
                        if (uploadedPhoto != null)
                        {

                            if (!string.IsNullOrEmpty(translate.Image))
                            {
                                string existingFilePath = Path.Combine(_env.WebRootPath, "bannergallery", translate.Image);
                                if (System.IO.File.Exists(existingFilePath))
                                {
                                    System.IO.File.Delete(existingFilePath);
                                }
                            }

                            string filename = Guid.NewGuid().ToString() + "_" + uploadedPhoto.FileName;
                            string path = FileHelper.GetFilePath(_env.WebRootPath, "bannergallery", filename);
                            await FileHelper.SaveFileAsync(path, uploadedPhoto);

                            translate.Image = filename;
                        }
                    }
                }

                _context.Update(banner);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
