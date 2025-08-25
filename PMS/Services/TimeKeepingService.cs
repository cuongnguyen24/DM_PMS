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
                

                // Lấy bảng gốc là bảng tblUsers với điều kiện là DateTime < Max time, và lấy distinct
                // Lấy dữ liệu chấm công theo thời gian - LINQ method syntax
                var timeKeepingQuery = _context.TblTimeKeepings
                    .Join(_context.TblUsers, tk => tk.UserId, u => u.Id, (tk, u) => new { tk, u })
                    .Join(_context.TblUserDepartments, x => x.u.Id, ud => ud.UserId, (x, ud) => new { x.tk, x.u, ud })
                    .Join(_context.TblDepartments, x => x.ud.DepartmentId, d => d.Id, (x, d) => new { x.tk, x.u, x.ud, d })
                    .Join(_context.TblUserPositions, x => x.u.Id, up => up.UserId, (x, up) => new { x.tk, x.u, x.ud, x.d, up })
                    .Join(_context.TblPositions, x => x.up.PositionId, p => p.Id, (x, p) => new { x.tk, x.u, x.ud, x.d, x.up, p })
                    .Where(x => x.tk.Year == request.Year 
                             && x.tk.Status == 1 
                             && x.u.Status == 1
                             && x.ud.Status == 1
                             && x.up.Status == 1
                             && x.p.Status == 1
                             && x.d.Status == 1)
                    .Select(x => new
                    {
                        UserId = x.u.Id,
                        FullName = x.u.Name,
                        UserCode = x.u.UserCode,
                        DepartmentId = x.d.Id,
                        DepartmentName = x.d.Name,
                        ShortName = x.d.ShortName,
                        PositionName = x.p.Name,
                        Month = x.tk.Month,
                        LV = x.tk.Lv,
                        H = x.tk.H,
                        P = x.tk.P,
                        L = x.tk.L,
                        OTS = x.tk.Ots,
                        CD = x.tk.Cd,
                        KL = x.tk.Kl
                    });

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
                
                // Lấy dữ liệu ca đêm - LINQ method syntax
                var nightShiftQuery = _context.TblNightShifts
                    .Join(_context.TblUsers, ns => ns.UserId, u => u.Id, (ns, u) => new { ns, u })
                    .Join(_context.TblUserDepartments, x => x.u.Id, ud => ud.UserId, (x, ud) => new { x.ns, x.u, ud })
                    .Join(_context.TblDepartments, x => x.ud.DepartmentId, d => d.Id, (x, d) => new { x.ns, x.u, x.ud, d })
                    .Where(x => x.ns.Year == request.Year 
                             && x.ns.Status == 1 
                             && x.u.Status == 1
                             && x.ud.Status == 1
                             && x.d.Status == 1)
                    .Select(x => new
                    {
                        UserId = x.u.Id,
                        FullName = x.u.Name,
                        UserCode = x.u.UserCode,
                        DepartmentId = x.d.Id,
                        DepartmentName = x.d.Name,
                        ShortName = x.d.ShortName,
                        Month = x.ns.Month,
                        Value = x.ns.Value
                    });

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
                
                // Lấy dữ liệu thêm giờ - LINQ method syntax
                var overtimeQuery = _context.TblOvertimes
                    .Join(_context.TblUsers, ot => ot.UserId, u => u.Id, (ot, u) => new { ot, u })
                    .Join(_context.TblUserDepartments, x => x.u.Id, ud => ud.UserId, (x, ud) => new { x.ot, x.u, ud })
                    .Join(_context.TblDepartments, x => x.ud.DepartmentId, d => d.Id, (x, d) => new { x.ot, x.u, x.ud, d })
                    .Where(x => x.ot.Year == request.Year 
                             && x.ot.Status == 1 
                             && x.u.Status == 1
                             && x.ud.Status == 1
                             && x.d.Status == 1)
                    .Select(x => new
                    {
                        UserId = x.u.Id,
                        FullName = x.u.Name,
                        UserCode = x.u.UserCode,
                        DepartmentId = x.d.Id,
                        DepartmentName = x.d.Name,
                        ShortName = x.d.ShortName,
                        Month = x.ot.Month,
                        Value = x.ot.Value
                    });

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

        public async Task<UpdateResponse> UpdateTimeKeepingAsync(UpdateTimeKeepingRequest request)
        {
            try
            {
                // Tìm hoặc tạo bản ghi TimeKeeping
                var timeKeeping = await _context.TblTimeKeepings
                    .FirstOrDefaultAsync(tk => tk.UserId == request.UserId 
                                            && tk.Month == request.Month 
                                            && tk.Year == request.Year);

                if (timeKeeping == null)
                {
                    // Tạo bản ghi mới
                    timeKeeping = new TblTimeKeeping
                    {
                        UserId = request.UserId,
                        Month = request.Month,
                        Year = request.Year,
                        Status = 1,
                        CreatedDate = DateTime.Now,
                        CreatedUser = "System",
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "System"
                    };
                    _context.TblTimeKeepings.Add(timeKeeping);
                }

                // Cập nhật field tương ứng
                switch (request.FieldType.ToUpper())
                {
                    case "LV":
                        timeKeeping.Lv = request.Value;
                        break;
                    case "H":
                        timeKeeping.H = request.Value;
                        break;
                    case "P":
                        timeKeeping.P = request.Value;
                        break;
                    case "L":
                        timeKeeping.L = request.Value;
                        break;
                    case "OTS":
                        timeKeeping.Ots = request.Value;
                        break;
                    case "CD":
                        timeKeeping.Cd = request.Value;
                        break;
                    case "KL":
                        timeKeeping.Kl = request.Value;
                        break;
                    default:
                        return new UpdateResponse
                        {
                            Success = false,
                            Message = $"Field type '{request.FieldType}' không hợp lệ."
                        };
                }

                timeKeeping.ModifiedDate = DateTime.Now;
                timeKeeping.ModifiedUser = "System";

                await _context.SaveChangesAsync();

                return new UpdateResponse
                {
                    Success = true,
                    Message = "Cập nhật dữ liệu thành công."
                };
            }
            catch (Exception ex)
            {
                return new UpdateResponse
                {
                    Success = false,
                    Message = $"Lỗi khi cập nhật dữ liệu: {ex.Message}"
                };
            }
        }

        public async Task<UpdateResponse> UpdateNightShiftAsync(UpdateNightShiftRequest request)
        {
            try
            {
                // Tìm hoặc tạo bản ghi NightShift
                var nightShift = await _context.TblNightShifts
                    .FirstOrDefaultAsync(ns => ns.UserId == request.UserId 
                                            && ns.Month == request.Month 
                                            && ns.Year == request.Year);

                if (nightShift == null)
                {
                    // Tạo bản ghi mới
                    nightShift = new TblNightShift
                    {
                        UserId = request.UserId,
                        Month = request.Month,
                        Year = request.Year,
                        Value = request.Value,
                        Status = 1,
                        CreatedDate = DateTime.Now,
                        CreatedUser = "System",
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "System"
                    };
                    _context.TblNightShifts.Add(nightShift);
                }
                else
                {
                    nightShift.Value = request.Value;
                    nightShift.ModifiedDate = DateTime.Now;
                    nightShift.ModifiedUser = "System";
                }

                await _context.SaveChangesAsync();

                return new UpdateResponse
                {
                    Success = true,
                    Message = "Cập nhật dữ liệu ca đêm thành công."
                };
            }
            catch (Exception ex)
            {
                return new UpdateResponse
                {
                    Success = false,
                    Message = $"Lỗi khi cập nhật dữ liệu ca đêm: {ex.Message}"
                };
            }
        }

        public async Task<UpdateResponse> UpdateOvertimeAsync(UpdateOvertimeRequest request)
        {
            try
            {
                // Tìm hoặc tạo bản ghi Overtime
                var overtime = await _context.TblOvertimes
                    .FirstOrDefaultAsync(ot => ot.UserId == request.UserId 
                                            && ot.Month == request.Month 
                                            && ot.Year == request.Year);

                if (overtime == null)
                {
                    // Tạo bản ghi mới
                    overtime = new TblOvertime
                    {
                        UserId = request.UserId,
                        Month = request.Month,
                        Year = request.Year,
                        Value = request.Value,
                        Status = 1,
                        CreatedDate = DateTime.Now,
                        CreatedUser = "System",
                        ModifiedDate = DateTime.Now,
                        ModifiedUser = "System"
                    };
                    _context.TblOvertimes.Add(overtime);
                }
                else
                {
                    overtime.Value = request.Value;
                    overtime.ModifiedDate = DateTime.Now;
                    overtime.ModifiedUser = "System";
                }

                await _context.SaveChangesAsync();

                return new UpdateResponse
                {
                    Success = true,
                    Message = "Cập nhật dữ liệu thêm giờ thành công."
                };
            }
            catch (Exception ex)
            {
                return new UpdateResponse
                {
                    Success = false,
                    Message = $"Lỗi khi cập nhật dữ liệu thêm giờ: {ex.Message}"
                };
            }
        }
    }
}
