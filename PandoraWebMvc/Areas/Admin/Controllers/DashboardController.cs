using Microsoft.AspNetCore.Mvc;

namespace PandoraWebMvc.Areas.Admin.Controllers
{
    public class DashboardController:Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            ViewBag.CurrentController = "Dashboard";
            ViewBag.CurrentAction = "Index";

            return View();
        }
    }
}
