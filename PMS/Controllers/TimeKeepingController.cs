using Microsoft.AspNetCore.Mvc;

namespace PMS.Controllers
{
    public class TimeKeepingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RenderLoaiCong(string loai, int nam)
        {
            // Tùy bạn lấy dữ liệu theo 'loai' và 'nam' để bind vào view model (nếu cần)
            switch (loai)
            {
                case "thoi_gian":
                    return PartialView("_TimeTable"); // truyền dữ liệu nếu cần
                case "ca_dem":
                    return PartialView("_NightTable");
                case "them_gio":
                    return PartialView("_OverTimeTable");
                default:
                    return PartialView("_TimeTable");
            }
        }

    }
}
