using Microsoft.AspNetCore.Mvc;
using PMS.Services;
using PMS.Models;
using System.Threading.Tasks;

namespace PMS.Controllers
{
    public class TimeKeepingController : Controller
    {
        private readonly ITimeKeepingService _timeKeepingService;

        public TimeKeepingController(ITimeKeepingService timeKeepingService)
        {
            _timeKeepingService = timeKeepingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> RenderLoaiCong(string loai, int nam, int quy = 0)
        {
            var request = new TimeKeepingRequest
            {
                Year = nam,
                Quarter = quy,
                Type = loai
            };

            try
            {
                switch (loai)
                {
                    case "thoi_gian":
                        var timeKeepingData = await _timeKeepingService.GetTimeKeepingDataAsync(request);
                        ViewBag.TimeKeepingData = timeKeepingData;
                        return PartialView("_TimeTable", timeKeepingData);
                        
                    case "ca_dem":
                        var nightShiftData = await _timeKeepingService.GetNightShiftDataAsync(request);
                        ViewBag.NightShiftData = nightShiftData;
                        return PartialView("_NightTable", nightShiftData);
                        
                    case "them_gio":
                        var overtimeData = await _timeKeepingService.GetOvertimeDataAsync(request);
                        ViewBag.OvertimeData = overtimeData;
                        return PartialView("_OverTimeTable", overtimeData);
                        
                    default:
                        var defaultData = await _timeKeepingService.GetTimeKeepingDataAsync(request);
                        ViewBag.TimeKeepingData = defaultData;
                        return PartialView("_TimeTable", defaultData);
                }
            }
            catch (System.Exception ex)
            {
                // Trả về partial view mặc định nếu có lỗi
                return PartialView("_TimeTable");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTimeKeeping([FromBody] UpdateTimeKeepingRequest request)
        {
            try
            {
                var result = await _timeKeepingService.UpdateTimeKeepingAsync(request);
                return Json(new { success = result.Success, message = result.Message });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật dữ liệu." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNightShift([FromBody] UpdateNightShiftRequest request)
        {
            try
            {
                var result = await _timeKeepingService.UpdateNightShiftAsync(request);
                return Json(new { success = result.Success, message = result.Message });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật dữ liệu ca đêm." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOvertime([FromBody] UpdateOvertimeRequest request)
        {
            try
            {
                var result = await _timeKeepingService.UpdateOvertimeAsync(request);
                return Json(new { success = result.Success, message = result.Message });
            }
            catch (System.Exception ex)
            {
                return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật dữ liệu thêm giờ." });
            }
        }
    }
}
