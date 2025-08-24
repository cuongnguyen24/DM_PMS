using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PMS.Models;

namespace PMS.Services
{
    public class TimeKeepingService : ITimeKeepingService
    {
        private readonly PMSDbContext _context;

        public TimeKeepingService(PMSDbContext context)
        {
            _context = context;
        }

        public async Task<TimeKeepingResponse> GetTimeKeepingDataAsync(TimeKeepingRequest request)
        {
            try
            {
                var response = new TimeKeepingResponse();
                
                // Lấy dữ liệu chấm công theo thời gian
                var timeKeepingQuery = from tk in _context.TblTimeKeepings
                                      join u in _context.TblUsers on tk.UserId equals u.Id
                                      join ud in _context.TblUserDepartments on u.Id equals ud.UserId
                                      join d in _context.TblDepartments on ud.DepartmentId equals d.Id
                                      join up in _context.TblUserPositions on u.Id equals up.UserId
                                      join p in _context.TblPositions on up.PositionId equals p.Id
                                      where tk.Year == request.Year 
                                            && tk.Status == 1 
                                            && u.Status == 1
                                            && ud.Status == 1
                                            && up.Status == 1
                                            && p.Status == 1
                                            && d.Status == 1
                                      select new
                                      {
                                          UserId = u.Id,
                                          FullName = u.Name,
                                          UserCode = u.UserCode,
                                          DepartmentId = d.Id,
                                          DepartmentName = d.Name,
                                          ShortName = d.ShortName,
                                          PositionName = p.Name,
                                          Month = tk.Month,
                                          LV = tk.Lv,
                                          H = tk.H,
                                          P = tk.P,
                                          L = tk.L,
                                          OTS = tk.Ots,
                                          CD = tk.Cd,
                                          KL = tk.Kl
                                      };

                // Lọc theo quý nếu cần
                if (request.Quarter > 0)
                {
                    var startMonth = (request.Quarter - 1) * 3 + 1;
                    var endMonth = request.Quarter * 3;
                    timeKeepingQuery = timeKeepingQuery.Where(x => x.Month >= startMonth && x.Month <= endMonth);
                }

                var timeKeepingData = await timeKeepingQuery.ToListAsync();

                // Nhóm theo phòng ban
                var departmentGroups = timeKeepingData
                    .GroupBy(x => new { x.DepartmentId, x.DepartmentName, x.ShortName })
                    .Select(g => new DepartmentGroup
                    {
                        DepartmentId = g.Key.DepartmentId,
                        DepartmentName = g.Key.DepartmentName,
                        ShortName = g.Key.ShortName,
                        Employees = g.GroupBy(x => new { x.UserId, x.FullName, x.UserCode, x.PositionName })
                            .Select(emp => new TimeKeepingData
                            {
                                UserId = emp.Key.UserId,
                                FullName = emp.Key.FullName,
                                UserCode = emp.Key.UserCode,
                                DepartmentName = g.Key.DepartmentName,
                                PositionName = emp.Key.PositionName,
                                MonthlyData = emp.ToDictionary(x => x.Month, x => new MonthData
                                {
                                    Month = x.Month,
                                    LV = x.LV ?? 0,
                                    H = x.H ?? 0,
                                    P = x.P ?? 0,
                                    L = x.L ?? 0,
                                    OTS = x.OTS ?? 0,
                                    CD = x.CD ?? 0,
                                    KL = x.KL ?? 0
                                })
                            })
                            .ToList()
                    })
                    .ToList();

                // Tính tổng cả năm cho mỗi nhân viên
                foreach (var dept in departmentGroups)
                {
                    foreach (var emp in dept.Employees)
                    {
                        emp.YearlyTotalLV = emp.MonthlyData.Values.Sum(x => x.LV);
                        emp.YearlyTotalH = emp.MonthlyData.Values.Sum(x => x.H);
                        emp.YearlyTotalP = emp.MonthlyData.Values.Sum(x => x.P);
                        emp.YearlyTotalL = emp.MonthlyData.Values.Sum(x => x.L);
                        emp.YearlyTotalOTS = emp.MonthlyData.Values.Sum(x => x.OTS);
                        emp.YearlyTotalCD = emp.MonthlyData.Values.Sum(x => x.CD);
                        emp.YearlyTotalKL = emp.MonthlyData.Values.Sum(x => x.KL);
                    }
                }

                response.DepartmentGroups = departmentGroups;
                response.TotalEmployees = departmentGroups.Sum(x => x.Employees.Count);
                response.Success = true;
                response.Message = "Lấy dữ liệu thành công";

                return response;
            }
            catch (Exception ex)
            {
                return new TimeKeepingResponse
                {
                    Success = false,
                    Message = $"Lỗi khi lấy dữ liệu: {ex.Message}"
                };
            }
        }

        public async Task<TimeKeepingResponse> GetNightShiftDataAsync(TimeKeepingRequest request)
        {
            try
            {
                var response = new TimeKeepingResponse();
                
                // Lấy dữ liệu ca đêm
                var nightShiftQuery = from ns in _context.TblNightShifts
                                     join u in _context.TblUsers on ns.UserId equals u.Id
                                     join ud in _context.TblUserDepartments on u.Id equals ud.UserId
                                     join d in _context.TblDepartments on ud.DepartmentId equals d.Id
                                     where ns.Year == request.Year 
                                           && ns.Status == 1 
                                           && u.Status == 1
                                           && ud.Status == 1
                                           && d.Status == 1
                                     select new
                                     {
                                         UserId = u.Id,
                                         FullName = u.Name,
                                         UserCode = u.UserCode,
                                         DepartmentId = d.Id,
                                         DepartmentName = d.Name,
                                         ShortName = d.ShortName,
                                         Month = ns.Month,
                                         Value = ns.Value
                                     };

                // Lọc theo quý nếu cần
                if (request.Quarter > 0)
                {
                    var startMonth = (request.Quarter - 1) * 3 + 1;
                    var endMonth = request.Quarter * 3;
                    nightShiftQuery = nightShiftQuery.Where(x => x.Month >= startMonth && x.Month <= endMonth);
                }

                var nightShiftData = await nightShiftQuery.ToListAsync();

                // Nhóm theo phòng ban
                var departmentGroups = nightShiftData
                    .GroupBy(x => new { x.DepartmentId, x.DepartmentName, x.ShortName })
                    .Select(g => new DepartmentGroup
                    {
                        DepartmentId = g.Key.DepartmentId,
                        DepartmentName = g.Key.DepartmentName,
                        ShortName = g.Key.ShortName,
                        Employees = g.GroupBy(x => new { x.UserId, x.FullName, x.UserCode })
                            .Select(emp => new TimeKeepingData
                            {
                                UserId = emp.Key.UserId,
                                FullName = emp.Key.FullName,
                                UserCode = emp.Key.UserCode,
                                DepartmentName = g.Key.DepartmentName,
                                MonthlyData = emp.ToDictionary(x => x.Month, x => new MonthData
                                {
                                    Month = x.Month,
                                    LV = x.Value ?? 0 // Sử dụng LV để lưu giá trị ca đêm
                                })
                            })
                            .ToList()
                    })
                    .ToList();

                // Tính tổng cả năm cho mỗi nhân viên
                foreach (var dept in departmentGroups)
                {
                    foreach (var emp in dept.Employees)
                    {
                        emp.YearlyTotalLV = emp.MonthlyData.Values.Sum(x => x.LV);
                    }
                }

                response.DepartmentGroups = departmentGroups;
                response.TotalEmployees = departmentGroups.Sum(x => x.Employees.Count);
                response.Success = true;
                response.Message = "Lấy dữ liệu ca đêm thành công";

                return response;
            }
            catch (Exception ex)
            {
                return new TimeKeepingResponse
                {
                    Success = false,
                    Message = $"Lỗi khi lấy dữ liệu ca đêm: {ex.Message}"
                };
            }
        }

        public async Task<TimeKeepingResponse> GetOvertimeDataAsync(TimeKeepingRequest request)
        {
            try
            {
                var response = new TimeKeepingResponse();
                
                // Lấy dữ liệu thêm giờ
                var overtimeQuery = from ot in _context.TblOvertimes
                                   join u in _context.TblUsers on ot.UserId equals u.Id
                                   join ud in _context.TblUserDepartments on u.Id equals ud.UserId
                                   join d in _context.TblDepartments on ud.DepartmentId equals d.Id
                                   where ot.Year == request.Year 
                                         && ot.Status == 1 
                                         && u.Status == 1
                                         && ud.Status == 1
                                         && d.Status == 1
                                   select new
                                   {
                                       UserId = u.Id,
                                       FullName = u.Name,
                                       UserCode = u.UserCode,
                                       DepartmentId = d.Id,
                                       DepartmentName = d.Name,
                                       ShortName = d.ShortName,
                                       Month = ot.Month,
                                       Value = ot.Value
                                   };

                // Lọc theo quý nếu cần
                if (request.Quarter > 0)
                {
                    var startMonth = (request.Quarter - 1) * 3 + 1;
                    var endMonth = request.Quarter * 3;
                    overtimeQuery = overtimeQuery.Where(x => x.Month >= startMonth && x.Month <= endMonth);
                }

                var overtimeData = await overtimeQuery.ToListAsync();

                // Nhóm theo phòng ban
                var departmentGroups = overtimeData
                    .GroupBy(x => new { x.DepartmentId, x.DepartmentName, x.ShortName })
                    .Select(g => new DepartmentGroup
                    {
                        DepartmentId = g.Key.DepartmentId,
                        DepartmentName = g.Key.DepartmentName,
                        ShortName = g.Key.ShortName,
                        Employees = g.GroupBy(x => new { x.UserId, x.FullName, x.UserCode })
                            .Select(emp => new TimeKeepingData
                            {
                                UserId = emp.Key.UserId,
                                FullName = emp.Key.FullName,
                                UserCode = emp.Key.UserCode,
                                DepartmentName = g.Key.DepartmentName,
                                MonthlyData = emp.ToDictionary(x => x.Month, x => new MonthData
                                {
                                    Month = x.Month,
                                    LV = x.Value ?? 0 // Sử dụng LV để lưu giá trị thêm giờ
                                })
                            })
                            .ToList()
                    })
                    .ToList();

                // Tính tổng cả năm cho mỗi nhân viên
                foreach (var dept in departmentGroups)
                {
                    foreach (var emp in dept.Employees)
                    {
                        emp.YearlyTotalLV = emp.MonthlyData.Values.Sum(x => x.LV);
                    }
                }

                response.DepartmentGroups = departmentGroups;
                response.TotalEmployees = departmentGroups.Sum(x => x.Employees.Count);
                response.Success = true;
                response.Message = "Lấy dữ liệu thêm giờ thành công";

                return response;
            }
            catch (Exception ex)
            {
                return new TimeKeepingResponse
                {
                    Success = false,
                    Message = $"Lỗi khi lấy dữ liệu thêm giờ: {ex.Message}"
                };
            }
        }

        public async Task<List<DepartmentGroup>> GetDepartmentGroupsAsync()
        {
            try
            {
                var departments = await _context.TblDepartments
                    .Where(d => d.Status == 1)
                    .OrderBy(d => d.Name)
                    .Select(d => new DepartmentGroup
                    {
                        DepartmentId = d.Id,
                        DepartmentName = d.Name,
                        ShortName = d.ShortName
                    })
                    .ToListAsync();

                return departments;
            }
            catch (Exception)
            {
                return new List<DepartmentGroup>();
            }
        }
    }
}
