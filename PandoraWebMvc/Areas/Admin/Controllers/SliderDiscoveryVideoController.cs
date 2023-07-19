using Domain;
using Domain.Entities.SlidersDiscoveryVideo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandoraWebMvc.Areas.Admin.ViewModels.SliderDiscoveryVideo;
using Service.Helpers;

namespace PandoraWebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderDiscoveryVideoController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public SliderDiscoveryVideoController(IWebHostEnvironment env, AppDbContext context)
        {
            _env = env;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "SliderDiscoveryVideo";
            ViewBag.CurrentAction = "Index";

            List<SliderDiscoveryVideo> sliders = await _context.SliderDiscoveryVideos
                                                .Include(s => s.SliderDiscoveryVideoTranslates
                                                 .Where(st =>st.LangCode == "az"))
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
        
        public async Task <IActionResult> Create(SliderDiscoveryVideoCreateVM model)
        {
            try
            {
                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string path = FileHelper.GetFilePath(_env.WebRootPath, "sliderdiscoveryvideogallery", fileName);
                await FileHelper.SaveFileAsync(path, model.Photo);

                SliderDiscoveryVideo newSlider = new SliderDiscoveryVideo
                {
                    Video = fileName
                };

                List<SliderDiscoveryVideoTranslate> sliderDiscoveryVideoTranslates = model.Translates
                                                    .Select(t => new SliderDiscoveryVideoTranslate
                                                    {
                                                        LangCode = t.LangCode,
                                                        Tittle = t.Tittle,
                                                        Description = t.Description,
                                                        Links = t.Links
                                                    }).ToList();
                newSlider.SliderDiscoveryVideoTranslates =sliderDiscoveryVideoTranslates;

                await _context.SliderDiscoveryVideos.AddAsync(newSlider);
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
            if (id is null)
                return BadRequest();
            var slider = await _context.SliderDiscoveryVideos
                              .Include(s => s.SliderDiscoveryVideoTranslates)
                              .FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null)
                return NotFound();

          _context.SliderDiscoveryVideoTranslates.RemoveRange(slider.SliderDiscoveryVideoTranslates);

          _context.SliderDiscoveryVideos.Remove(slider);

          await _context.SaveChangesAsync();

          string path = FileHelper.GetFilePath(_env.WebRootPath, "sliderdiscoveryvideogallery", slider.Video);
          FileHelper.DeleteFile(path);

            //return RedirectToAction("Index");
            return Ok();
         
        }

        [HttpGet]
        public async Task <IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();
            var slider = await _context.SliderDiscoveryVideos
               .Include(s => s.SliderDiscoveryVideoTranslates)
               .FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null)
                return NotFound();

            SliderDiscoveryVideoUpdateVM model = new SliderDiscoveryVideoUpdateVM
            {
                Id = slider.Id,
                Video = slider.Video,
                Translates = slider.SliderDiscoveryVideoTranslates.Select(t => new SliderDiscoveryVideoTranslateVM
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
        public async Task<IActionResult> Edit(SliderDiscoveryVideoUpdateVM model, IFormFile NewVideo)
        {
            try
            {
                var slider = await _context.SliderDiscoveryVideos
                    .Include(s => s.SliderDiscoveryVideoTranslates)
                    .FirstOrDefaultAsync(s => s.Id == model.Id);

                if (slider is null)
                    return NotFound();

                string videoPath = null;

                if (NewVideo != null)
                {
                    if (!string.IsNullOrEmpty(slider.Video))
                    {
                        videoPath = $"{_env.WebRootPath}/sliderdiscoveryvideogallery/{slider.Video}";
                        if (System.IO.File.Exists(videoPath))
                        {
                            System.IO.File.Delete(videoPath);
                        }
                    }

                    var videoName = Guid.NewGuid().ToString() + Path.GetExtension(NewVideo.FileName);
                    videoPath = $"{_env.WebRootPath}/sliderdiscoveryvideogallery/{videoName}";

                    using (var stream = new FileStream(videoPath, FileMode.Create))
                    {
                        await NewVideo.CopyToAsync(stream);
                    }

                    slider.Video = videoName;
                }

                foreach (var translate in model.Translates)
                {
                    var existingTranslate = slider.SliderDiscoveryVideoTranslates
                        .FirstOrDefault(t => t.LangCode == translate.LangCode);

                    if (existingTranslate != null)
                    {
                        existingTranslate.Tittle = translate.Tittle;
                        existingTranslate.Description = translate.Description;
                        existingTranslate.Links = translate.Links;
                    }
                    else
                    {
                        SliderDiscoveryVideoTranslate newTranslate = new SliderDiscoveryVideoTranslate()
                        {
                            LangCode = translate.LangCode,
                            Tittle = translate.Tittle,
                            Description = translate.Description,
                            Links = translate.Links
                        };
                        slider.SliderDiscoveryVideoTranslates.Add(newTranslate);
                    }
                }

                foreach (var existingTranslate in slider.SliderDiscoveryVideoTranslates.ToList())
                {
                    if (!model.Translates.Any(t => t.LangCode == existingTranslate.LangCode))
                        _context.SliderDiscoveryVideoTranslates.Remove(existingTranslate);
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

            var slider = await _context.SliderDiscoveryVideos
                               .Include(s => s.SliderDiscoveryVideoTranslates)
                               .FirstOrDefaultAsync(s => s.Id == id);

            if (slider is null)
                return NotFound();

            SliderDiscoveryVideoDetailVM model = new SliderDiscoveryVideoDetailVM
            {
                Id = slider.Id,
                Video = slider.Video,
                Translates = slider.SliderDiscoveryVideoTranslates.Select(t => new SliderDiscoveryVideoTranslateVM
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
