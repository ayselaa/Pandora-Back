using Domain;
using Domain.Entities.FooterMenus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PandoraWebMvc.Areas.Admin.ViewModels.FooterMenuItem;
using System.Text.RegularExpressions;

namespace PandoraWebMvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FooterMenuItemController : Controller
    {
        private readonly AppDbContext _context;
        public FooterMenuItemController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.CurrentController = "FooterMenuItem";
            ViewBag.CurrentAction = "Index";
            List<FooterItem> menus = await _context.FooterItems
                .Include(m => m.FooterMenu.Translates)
                .Include(m => m.FooterItemTranslates
                 .Where(mt=> mt.LangCode == "en")) 
                .OrderByDescending(m=>m.Id)
                .ToListAsync();

            List<FooterMenuItemVM> viewModel = menus.Select(item =>
            {
                var translation = item.FooterMenu?.Translates?.FirstOrDefault(t => t.LangCode == "en");

                var firstTranslation = item.FooterItemTranslates?.FirstOrDefault();

                return new FooterMenuItemVM
                {
                    Id = item.Id,
                    FooterMenuName = translation?.Name,
                    Translate = item.FooterItemTranslates?.Select(translate => new FooterMenuItemTranslateVM
                    {
                        LangCode = translate.LangCode,
                        Name = translate.Name,
                        Description = translate.Description
                    }).ToList() ?? new List<FooterMenuItemTranslateVM>()
                };
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
           var data = await _context.FooterMenus.Select(fm => new FooterDto
                    {
                        Id = fm.Id,
                        Name = fm.Translates.Where(t => t.LangCode == "en").FirstOrDefault().Name
                    }).ToListAsync();
            ViewBag.Menus = new SelectList(data, "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FooterMenuItemCreateVM model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                   
                    var data = await _context.FooterMenus.Select(fm => new FooterDto
                    {
                        Id = fm.Id,
                        Name = fm.Translates.Where(t => t.LangCode == "en").FirstOrDefault().Name
                    }).ToListAsync();
                    
                    ViewBag.Menus = data;
                    return View();
                }


                List<FooterItemTranslate> footerItemTranslates = model.Translates
                    .Select(translate => new FooterItemTranslate
                    {
                        
                        LangCode = translate.LangCode,
                        Name = translate.Name,
                        Description = translate.Name
                    })
                    .ToList();

                FooterItem newFooterItem = new FooterItem
                {
                    FooterItemTranslates = footerItemTranslates,
                    SoftDelete = false,
                    FooterMenuId = model.FooterMenuId

                };

                await _context.FooterItems.AddAsync(newFooterItem);
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
            var menu = await _context.FooterItems
                           .Include(m => m.FooterItemTranslates)
                           .FirstOrDefaultAsync(m => m.Id == id);
           if (menu is null)
                return NotFound();

            _context.FooterItemTranslates.RemoveRange(menu.FooterItemTranslates);
            _context.FooterItems.Remove(menu);

            await _context.SaveChangesAsync();

            //return RedirectToAction("Index");
            return Ok();
           
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var footerItem = await _context.FooterItems
                .Include(fi => fi.FooterItemTranslates)
                .FirstOrDefaultAsync(fi => fi.Id == id);

            if (footerItem == null)
            {
                return NotFound();
            }

            var data = await _context.FooterMenus
                .Select(fm => new FooterDto
                {
                    Id = fm.Id,
                    Name = fm.Translates.Where(t => t.LangCode == "en").FirstOrDefault().Name
                })
                .ToListAsync();

            var viewModel = new FooterMenuItemUpdateVM
            {
                Id = footerItem.Id,
                FooterMenuId = footerItem.FooterMenuId,
                Translates = footerItem.FooterItemTranslates?
                    .Select(translate => new FooterMenuItemTranslateVM
                    {
                        LangCode = translate.LangCode,
                        Name = translate.Name,
                        Description = translate.Description
                    })
                    .ToList() ?? new List<FooterMenuItemTranslateVM>()
            };

            if (viewModel.Translates == null)
            {
                viewModel.Translates = new List<FooterMenuItemTranslateVM>();
            }

            ViewBag.Menus = new SelectList(data, "Id", "Name");

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task <IActionResult> Edit (int id, FooterMenuItemUpdateVM model)
        {
            if (id != model.Id)
                return NotFound();

            if (!ModelState.IsValid)
            {
                var data = await _context.FooterMenus.Select(fm => new FooterDto
                {
                    Id = fm.Id,
                    Name = fm.Translates.Where(t => t.LangCode == "en").FirstOrDefault().Name
                }).ToListAsync();

                ViewBag.Menus = new SelectList(data, "Id", "Name");
                return View(model);

               
            }
            var footerItem = await _context.FooterItems
                                    .Include(fi => fi.FooterItemTranslates)
                                    .FirstOrDefaultAsync(fi => fi.Id == id);
            if(footerItem is null)
              return NotFound();
            footerItem.FooterMenuId = model.FooterMenuId;

            foreach (var translate in model.Translates)
            {
                var footerItemTranslate = footerItem.FooterItemTranslates
                                        .FirstOrDefault(ft => ft.LangCode == translate.LangCode);
                if(footerItemTranslate != null)
                {
                    footerItemTranslate.Name = translate.Name;
                    footerItemTranslate.Description = translate.Description;
                    
                }
                else
                {
                    footerItem.FooterItemTranslates.Add(new FooterItemTranslate
                    {
                        LangCode = translate.LangCode,
                        Name = translate.Name,
                        Description = translate.Description
                    });
                }
            }
            var removedTranslations = footerItem.FooterItemTranslates
                                  .Where(ft => !model.Translates.Any(mt => mt.LangCode == ft.LangCode))
                                   .ToList();
            foreach (var translation in removedTranslations)
            {
                footerItem.FooterItemTranslates.Remove(translation);
            }
            try
            {
                _context.Update(footerItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FooterItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
                
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
          
            FooterItem footerItem = await _context.FooterItems
                .Include(m => m.FooterMenu.Translates)
                .Include(m => m.FooterItemTranslates) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (footerItem == null)
            {
               
                return NotFound();
            }

    
            var translation = footerItem.FooterMenu?.Translates?.FirstOrDefault(t => t.LangCode == "en");

          


            var viewModel = new FooterMenuItemDetailVM
            {
             
                MenuName = translation?.Name,
                Translates = footerItem.FooterItemTranslates?.Select(translate => new FooterMenuItemTranslateVM
                {
                    LangCode = translate.LangCode,
                    Name = translate.Name,
                    //Description=translate.Description
                    Description = Regex.Replace(translate.Description, "<.*?>", string.Empty)
                }).ToList(),
     
            };

            return View(viewModel);
        }




        private bool FooterItemExists(int id)
        {
            return _context.FooterItems.Any(e => e.Id == id);
        }
    }

    public class FooterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
