using Microsoft.AspNetCore.Mvc;

namespace PMS.Controllers
{
    public class TimeOffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
