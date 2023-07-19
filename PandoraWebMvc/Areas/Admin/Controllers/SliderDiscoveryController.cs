using Domain;
using Domain.Entities.SliderDiscovery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandoraWebMvc.Areas.Admin.ViewModels.SliderDiscovery;
using Service.Helpers;

namespace PandoraWebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderDiscoveryController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public SliderDiscoveryController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "SliderDiscovery";
            ViewBag.CurrentAction = "Index";

            List<SliderDiscovery> sliders = await _context.SliderDiscoveries
                                            .Include(s=> s.SliderDiscoveryTranslates
                                             .Where (st=> st.LangCode == "az"))
                                            .OrderByDescending(s=>s.Id)
                                            .ToListAsync();
            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task <IActionResult> Create(SliderDiscoveryCreateVM model)
        {
            try
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "sliderdiscoverygallery", fileName);
                await FileHelper.SaveFileAsync(path, model.Photo);

                SliderDiscovery newSlider = new SliderDiscovery
                {
                    Image = fileName
                };

                List<SliderDiscoveryTranslate> sliderDiscoveryTranslates = model.Translates
                                                                       .Select(t => new SliderDiscoveryTranslate
                                                                       {
                                                                           LangCode = t.LangCode,
                                                                           Tittle = t.Tittle,
                                                                           Description = t.Description,
                                                                           Links = t.Links
                                                                       }).ToList();

                newSlider.SliderDiscoveryTranslates = sliderDiscoveryTranslates;
                await _context.SliderDiscoveries.AddAsync(newSlider);
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

        public async Task <IActionResult> Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var slider = await  _context.SliderDiscoveries
                                 .Include(s => s.SliderDiscoveryTranslates)
                                 .FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null)
                return NotFound();

            _context.SliderDiscoveriesTranslates.RemoveRange(slider.SliderDiscoveryTranslates);
            _context.SliderDiscoveries.Remove(slider);

            await _context.SaveChangesAsync();

            string path = FileHelper.GetFilePath(_env.WebRootPath, "sliderdiscoverygallery", slider.Image);
            FileHelper.DeleteFile(path);

            //return RedirectToAction("Index");

            return Ok();

        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var slider = await _context.SliderDiscoveries
               .Include(s => s.SliderDiscoveryTranslates)
               .FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null)
                return NotFound();

            SliderDiscoveryUpdateVM model = new SliderDiscoveryUpdateVM
            {
                Id = slider.Id,
                Photo = slider.Image,
                Translates = slider.SliderDiscoveryTranslates.Select(t => new SliderDiscoveryTranslateVM
                {
                    LangCode = t.LangCode,
                    Tittle = t.Tittle,
                    Description = t.Description,
                    Links = t.Links
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderDiscoveryUpdateVM model, IFormFile NewPhoto)
        {
            try
            {
                var slider = await _context.SliderDiscoveries
                    .Include(s => s.SliderDiscoveryTranslates)
                    .FirstOrDefaultAsync(s => s.Id == model.Id);

                if (slider is null)
                    return NotFound();

                string photoPath = null;

                if (NewPhoto != null)
                {
                    if (!string.IsNullOrEmpty(slider.Image))
                    {
                        photoPath = $"{_env.WebRootPath}/sliderdiscoverygallery/{slider.Image}";
                        if (System.IO.File.Exists(photoPath))
                        {
                            System.IO.File.Delete(photoPath);
                        }
                    }

                    var photoName = Guid.NewGuid().ToString() + Path.GetExtension(NewPhoto.FileName);
                    photoPath = $"{_env.WebRootPath}/sliderdiscoverygallery/{photoName}";

                    using (var stream = new FileStream(photoPath, FileMode.Create))
                    {
                        await NewPhoto.CopyToAsync(stream);
                    }

                    slider.Image = photoName;
                }

                foreach (var translate in model.Translates)
                {
                    var existingTranslate = slider.SliderDiscoveryTranslates
                        .FirstOrDefault(t => t.LangCode == translate.LangCode);

                    if (existingTranslate != null)
                    {
                        existingTranslate.Tittle = translate.Tittle;
                        existingTranslate.Description = translate.Description;
                        existingTranslate.Links = translate.Links;
                    }
                    else
                    {
                        SliderDiscoveryTranslate newTranslate = new SliderDiscoveryTranslate()
                        {
                            LangCode = translate.LangCode,
                            Tittle = translate.Tittle,
                            Description = translate.Description,
                            Links = translate.Links
                        };
                        slider.SliderDiscoveryTranslates.Add(newTranslate);
                    }
                }

                foreach (var existingTranslate in slider.SliderDiscoveryTranslates.ToList())
                {
                    if (!model.Translates.Any(t => t.LangCode == existingTranslate.LangCode))
                        _context.SliderDiscoveriesTranslates.Remove(existingTranslate);
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
        public async Task <IActionResult> Detail(int? id)
        {
            if (id is null)
                return BadRequest();
            var slider = await _context.SliderDiscoveries
                         .Include(s=>s.SliderDiscoveryTranslates)
                         .FirstOrDefaultAsync(s=> s.Id== id);
            if (slider is null)
                return NotFound();

            SliderDiscoveryDetailVM model = new SliderDiscoveryDetailVM
            {
                Id = slider.Id,
                Photo = slider.Image,
                Translates = slider.SliderDiscoveryTranslates.Select(t => new SliderDiscoveryTranslateVM
                {
                    LangCode = t.LangCode,
                    Tittle = t.Tittle,
                    Description = t.Description,
                    Links = t.Links
                }).ToList()
            };

            return View(model);
        }

    }
}
