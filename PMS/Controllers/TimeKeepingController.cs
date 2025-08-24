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
            // Log để debug
            System.Diagnostics.Debug.WriteLine($"RenderLoaiCong called with loai: {loai}, nam: {nam}, quy: {quy}");
            
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
                System.Diagnostics.Debug.WriteLine($"Error in RenderLoaiCong: {ex.Message}");
                // Trả về partial view mặc định nếu có lỗi
                return PartialView("_TimeTable");
            }
        }
    }
}
