using Microsoft.AspNetCore.Mvc;

namespace PMS.Controllers
{
    public class SalaryPaymentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RenderLoaiDoiTuong(string loai, int nam)
        {
            switch (loai)
            {
                case "NQL":
                    return PartialView("_ManagerTable", nam);
                case "NLD":
                    return PartialView("_EmployeeTable", nam);
                default:
                    return PartialView("_ManagerTable", nam);
            }
        }
    }
}
