using Domain;
using Domain.Entities.BannerGifts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandoraWebMvc.Areas.Admin.ViewModels.BannerGift;
using Service.Helpers;

namespace PandoraWebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BannerGiftController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public BannerGiftController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "BannerGift";
            ViewBag.CurrentAction = "Index";

            List<BannerGift> bannerGifts = await _context.BannerGifts
                                           .Include(bg=>bg.BannerGiftTranslates
                                           .Where (bt => bt.LangCode == "az"))
                                           .OrderByDescending(bg => bg.Id)
                                           .ToListAsync();
            return View(bannerGifts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(BannerGiftCreateVM model)
        {
            try
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "bannergiftgallery", fileName);
                await FileHelper.SaveFileAsync(path, model.Photo);

                BannerGift newBannerGift = new BannerGift
                {
                    Image = fileName
                };

                List<BannerGiftTranslate> bannerGiftTranslates = model.Translates.Select(translate => new BannerGiftTranslate
                {
                    LangCode = translate.LangCode,
                    Tittle = translate.Tittle,
                    Description = translate.Description,
                    Link = translate.Links
                }).ToList();

                newBannerGift.BannerGiftTranslates = bannerGiftTranslates;

                await _context.BannerGifts.AddAsync(newBannerGift);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest();

                var bannerGift = await _context.BannerGifts
                    .Include(b=>b.BannerGiftTranslates)
                    .FirstOrDefaultAsync(b => b.Id == id);

                if (bannerGift is null)
                    return NotFound();


                _context.BannerGiftTranslates.RemoveRange(bannerGift.BannerGiftTranslates);

                await _context.SaveChangesAsync();


                _context.BannerGifts.Remove(bannerGift);

                await _context.SaveChangesAsync();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "bannergiftgallery", bannerGift.Image);
                FileHelper.DeleteFile(path);


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

            var bannerGift = await _context.BannerGifts
                .Include(s => s.BannerGiftTranslates)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (bannerGift is null)
                return NotFound();

            BannerGiftUpdateVM model = new BannerGiftUpdateVM
            {
                Id = bannerGift.Id,
                Photo = bannerGift.Image,
                Translates = bannerGift.BannerGiftTranslates.Select(t => new BannerGiftTranslateVM
                {
                    LangCode = t.LangCode,
                    Tittle =t.Tittle,
                    Description = t.Description,
                    Links = t.Link
                   
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BannerGiftUpdateVM model, IFormFile NewImage)
        {
            try
            {
                var bannerGift = await _context.BannerGifts
                 .Include(s => s.BannerGiftTranslates)
                 .FirstOrDefaultAsync(s => s.Id == model.Id);

                if (bannerGift is null)
                    return NotFound();

                string imagePath = null;

                if (NewImage != null)
                {

                    if (!string.IsNullOrEmpty(bannerGift.Image))
                    {
                        imagePath = $"{_env.WebRootPath}/bannergiftgallery/{bannerGift.Image}";
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    // Save the new image
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(NewImage.FileName);
                    imagePath = $"{_env.WebRootPath}/bannergiftgallery/{imageName}";
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(stream);
                    }

                    bannerGift.Image = imageName;
                }

                foreach (var translate in model.Translates)
                {
                    var existingTranslate = bannerGift.BannerGiftTranslates
                                                   .FirstOrDefault(t => t.LangCode == translate.LangCode);
                    if (existingTranslate != null)
                    {
                        existingTranslate.Tittle = translate.Tittle;
                        existingTranslate.Description = translate.Description;
                        existingTranslate.Link = translate.Links;

                    }
                    else
                    {
                        BannerGiftTranslate newTranslate = new BannerGiftTranslate()
                        {
                            LangCode = translate.LangCode,
                            Tittle = translate.Tittle,
                            Description = translate.Description,
                            Link = translate.Links
                        };
                        bannerGift.BannerGiftTranslates.Add(newTranslate);
                    }
                }
                foreach (var existingTranslate in bannerGift.BannerGiftTranslates.ToList())
                {
                    if (!model.Translates.Any(t => t.LangCode == existingTranslate.LangCode))
                        _context.BannerGiftTranslates.Remove(existingTranslate);
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
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null)
                return BadRequest();

            var bannerGift = await _context.BannerGifts
                .Include(s => s.BannerGiftTranslates)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (bannerGift is null)
                return NotFound();

            BannerGiftDetailVM model = new BannerGiftDetailVM
            {
                Id = bannerGift.Id,
                Image = bannerGift.Image,
                Translates = bannerGift.BannerGiftTranslates.Select(t => new BannerGiftTranslateVM
                {
                    LangCode = t.LangCode,
                    Tittle = t.Tittle,
                    Description = t.Description,
                    Links = t.Link
                }).ToList()
            };

            return View(model);
        }

    }
}
