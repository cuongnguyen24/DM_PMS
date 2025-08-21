using Microsoft.AspNetCore.Mvc;

namespace PMS.Controllers
{
    public class ElectricalSafetyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
