using Domain;
using Domain.Entities.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandoraWebMvc.Areas.Admin.ViewModels.Slider;
using Service.Helpers;

namespace PandoraWebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "Slider";
            ViewBag.CurrentAction = "Index";

            List<Slider> sliders = await _context.Sliders
                                                 .Include(s => s.SlidersTranslates
                                                 .Where(st => st.LangCode == "az"))
                                                 .OrderByDescending(s => s.Id)
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

        public async Task<IActionResult> Create(SliderCreateVM model)
        {
            try
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "slidergallery", fileName);
                await FileHelper.SaveFileAsync(path, model.Photo);

                Slider newSlider = new Slider
                {
                    Image = fileName
                };

                List<SlidersTranslate> slidersTranslates = model.Translates.Select(translate => new SlidersTranslate
                {
                    LangCode = translate.LangCode,
                    Tittle = translate.Tittle,
                    Description = translate.Description,
                    Links = translate.Links
                }).ToList();

                newSlider.SlidersTranslates = slidersTranslates;

                await _context.Sliders.AddAsync(newSlider);
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

                var slider = await _context.Sliders
                    .Include(s => s.SlidersTranslates)
                    .FirstOrDefaultAsync(s => s.Id == id);

                if (slider == null)
                    return NotFound();


                _context.SlidersTranslates.RemoveRange(slider.SlidersTranslates);

                await _context.SaveChangesAsync();


                _context.Sliders.Remove(slider);

                await _context.SaveChangesAsync();

                string path = FileHelper.GetFilePath(_env.WebRootPath, "slidergallery", slider.Image);
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

            var slider = await _context.Sliders
                .Include(s => s.SlidersTranslates)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (slider is null)
                return NotFound();

            SliderUpdateVM model = new SliderUpdateVM
            {
                Id = slider.Id,
                Image = slider.Image,
                Translates = slider.SlidersTranslates.Select(t => new SliderTranslateVM
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
        public async Task<IActionResult> Edit(SliderUpdateVM model, IFormFile NewImage)
        {
            try
            {
                var slider = await _context.Sliders
                    .Include(s => s.SlidersTranslates)
                    .FirstOrDefaultAsync(s => s.Id == model.Id);

                if (slider == null)
                    return NotFound();

                string imagePath = null;

                if (NewImage != null)
                {

                    if (!string.IsNullOrEmpty(slider.Image))
                    {
                        imagePath = $"{_env.WebRootPath}/slidergallery/{slider.Image}";
                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                    }

                    // Save the new image
                    var imageName = Guid.NewGuid().ToString() + Path.GetExtension(NewImage.FileName);
                    imagePath = $"{_env.WebRootPath}/slidergallery/{imageName}";
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        await NewImage.CopyToAsync(stream);
                    }

                    slider.Image = imageName;
                }

                foreach (var translate in model.Translates)
                {
                    var existingTranslate = slider.SlidersTranslates
                                                   .FirstOrDefault(t => t.LangCode == translate.LangCode);
                    if (existingTranslate != null)
                    {
                        existingTranslate.Tittle = translate.Tittle;
                        existingTranslate.Description = translate.Description;
                        existingTranslate.Links = translate.Links;

                    }
                    else
                    {
                        SlidersTranslate newTranslate = new SlidersTranslate()
                        {
                            LangCode = translate.LangCode,
                            Tittle = translate.Tittle,
                            Description = translate.Description,
                            Links = translate.Links
                        };
                        slider.SlidersTranslates.Add(newTranslate);
                    }
                }
                foreach (var existingTranslate in slider.SlidersTranslates.ToList())
                {
                    if (!model.Translates.Any(t => t.LangCode == existingTranslate.LangCode))
                        _context.SlidersTranslates.Remove(existingTranslate);
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

            var slider = await _context.Sliders
                               .Include(s => s.SlidersTranslates)
                               .FirstOrDefaultAsync(s => s.Id == id);

            if (slider is null)
                return NotFound();

            SliderDetailVM model = new SliderDetailVM
            {
                Id = slider.Id,
                Image = slider.Image,
                Translates = slider.SlidersTranslates.Select(t => new SliderTranslateVM
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
