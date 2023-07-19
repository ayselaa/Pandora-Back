using Domain;
using Domain.Entities.FooterMenus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PandoraWebMvc.Areas.Admin.ViewModels.FooterMenu;

namespace PandoraWebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FooterMenuController : Controller
    {
        private readonly AppDbContext _context;
        public FooterMenuController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "FooterMenu";
            ViewBag.CurrentAction = "Index";

            List<FooterMenu> menus = await _context.FooterMenus
                                     .Include(m => m.Translates
                                      .Where(mt => mt.LangCode == "az"))
                                     .OrderByDescending(m=>m.Id)
                                     .ToListAsync();
            return View(menus);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(FooterMenuCreateVM model)
        {
            try
            {
                List<FooterMenuTranslate> footerMenuTranslates = model.Translates.Select(t => new FooterMenuTranslate
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                }).ToList();

                FooterMenu footerMenu = new FooterMenu();

                footerMenu.Translates = footerMenuTranslates;

                await _context.FooterMenus.AddAsync(footerMenu);
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
                var model = await _context.FooterMenus
                           .Where(m => !m.SoftDelete)
                            .Include(m => m.Translates)
                            .FirstOrDefaultAsync(m => m.Id == id);
                if (model is null)
                    return NotFound();

                _context.FooterMenuTranslates.RemoveRange(model.Translates);

                _context.FooterMenus.Remove(model);

                await _context.SaveChangesAsync();

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
            var menu = await _context.FooterMenus
                         .Where(m => !m.SoftDelete)
                          .Include(m => m.Translates)
                          .FirstOrDefaultAsync(m => m.Id == id);
            if (menu is null)
                return NotFound();
            FooterMenuUpdateVM model = new FooterMenuUpdateVM
            {
                Id = menu.Id,
                Translates = menu.Translates.Select(t => new FooterMenuTranslateVM
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                }).ToList()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FooterMenuUpdateVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var menu = await _context.FooterMenus
                    .Where(m => !m.SoftDelete)
                    .Include(m => m.Translates)
                    .FirstOrDefaultAsync(m => m.Id == model.Id);

                if (menu is null)
                {
                    return NotFound();
                }

                foreach (var item in model.Translates)
                {
                    var translate = menu.Translates.FirstOrDefault(t => t.LangCode == item.LangCode);
                    if (translate != null)
                    {
                        translate.Name = item.Name;
                    }
                    else
                    {
                        menu.Translates.Add(new FooterMenuTranslate
                        {
                            LangCode = item.LangCode,
                            Name = item.Name
                        });
                    }
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
                return BadRequest(0);
            var menu = await _context.FooterMenus
                     .Where(m => !m.SoftDelete)
                     .Include(m => m.Translates)
                     .FirstOrDefaultAsync(m => m.Id == id);
            if(menu is null)
                return NotFound();

            FooterMenuDetailVM model = new FooterMenuDetailVM
            {
                Id = menu.Id,
                Translates = menu.Translates.Select(t => new FooterMenuTranslateVM
                {
                    LangCode = t.LangCode,
                    Name = t.Name,
                }).ToList()
            };
            return View(model);
        }
        [HttpPost]
        public async Task <IActionResult> SetStatus(int? id)
        {
            if (id is null)
                return BadRequest();
            var menu = await _context.FooterMenus
                             .Include(m => m.Translates)
                             .FirstOrDefaultAsync(m => m.Id == id);
            if(menu is null)
                return NotFound();
            menu.SoftDelete = !menu.SoftDelete;
            await _context.SaveChangesAsync();
            return Ok(menu.SoftDelete);

        }

    }
}
