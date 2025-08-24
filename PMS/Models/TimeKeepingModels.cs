using System;
using System.Collections.Generic;

namespace PMS.Models
{
    // Model cho dữ liệu chấm công theo thời gian
    public class TimeKeepingData
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserCode { get; set; }
        public string DepartmentName { get; set; }
        public string PositionName { get; set; }
        public Dictionary<int, MonthData> MonthlyData { get; set; } = new Dictionary<int, MonthData>();
        public decimal YearlyTotalLV { get; set; }
        public decimal YearlyTotalH { get; set; }
        public decimal YearlyTotalP { get; set; }
        public decimal YearlyTotalL { get; set; }
        public decimal YearlyTotalOTS { get; set; }
        public decimal YearlyTotalCD { get; set; }
        public decimal YearlyTotalKL { get; set; }
    }

    // Model cho dữ liệu theo tháng
    public class MonthData
    {
        public int Month { get; set; }
        public decimal LV { get; set; }      // Số ngày làm việc
        public decimal H { get; set; }       // Số H
        public decimal P { get; set; }       // Số phép
        public decimal L { get; set; }       // Số L
        public decimal OTS { get; set; }     // Số OTS
        public decimal CD { get; set; }      // Số CD
        public decimal KL { get; set; }      // Số KL
    }

    // Model cho dữ liệu ca đêm
    public class NightShiftData
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserCode { get; set; }
        public string DepartmentName { get; set; }
        public Dictionary<int, decimal> MonthlyData { get; set; } = new Dictionary<int, decimal>();
        public decimal YearlyTotal { get; set; }
    }

    // Model cho dữ liệu thêm giờ
    public class OvertimeData
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserCode { get; set; }
        public string DepartmentName { get; set; }
        public Dictionary<int, decimal> MonthlyData { get; set; } = new Dictionary<int, decimal>();
        public decimal YearlyTotal { get; set; }
    }

    // Model cho nhóm phòng ban
    public class DepartmentGroup
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string ShortName { get; set; }
        public List<TimeKeepingData> Employees { get; set; } = new List<TimeKeepingData>();
    }

    // Model cho request chấm công
    public class TimeKeepingRequest
    {
        public int Year { get; set; }
        public int Quarter { get; set; } // 0 = cả năm, 1-4 = quý cụ thể
        public string Type { get; set; } // "thoi_gian", "ca_dem", "them_gio"
    }

    // Model cho response chấm công
    public class TimeKeepingResponse
    {
        public List<DepartmentGroup> DepartmentGroups { get; set; } = new List<DepartmentGroup>();
        public int TotalEmployees { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
