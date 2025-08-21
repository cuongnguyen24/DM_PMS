using Microsoft.AspNetCore.Mvc;

namespace PMS.Controllers
{
    public class AllowancesManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        // Trả về partial view theo loại công và năm
        public IActionResult RenderLoaiPhuTro(string loai, int nam)
        {
            switch (loai)
            {
                case "dien_thoai":
                    return PartialView("_PhoneTable", nam);
                case "cong_doan_phi":
                    return PartialView("_UnionFeeTable", nam);
                case "thue_thu_nhap_ca_nhan":
                    return PartialView("_IncomeTaxTable", nam);
                default:
                    return PartialView("_PhoneTable", nam);
            }
        }
    }
}
