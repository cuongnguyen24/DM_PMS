using System.Collections.Generic;
using System.Threading.Tasks;
using PMS.Models;

namespace PMS.Services
{
    public interface ITimeKeepingService
    {
        /// <summary>
        /// Lấy dữ liệu chấm công theo thời gian
        /// </summary>
        Task<TimeKeepingResponse> GetTimeKeepingDataAsync(TimeKeepingRequest request);

        /// <summary>
        /// Lấy dữ liệu chấm công ca đêm
        /// </summary>
        Task<TimeKeepingResponse> GetNightShiftDataAsync(TimeKeepingRequest request);

        /// <summary>
        /// Lấy dữ liệu chấm công thêm giờ
        /// </summary>
        Task<TimeKeepingResponse> GetOvertimeDataAsync(TimeKeepingRequest request);

        /// <summary>
        /// Lấy danh sách nhóm phòng ban
        /// </summary>
        Task<List<DepartmentGroup>> GetDepartmentGroupsAsync();
    }
}
