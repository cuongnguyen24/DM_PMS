using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblAdvanceSalaries = new HashSet<TblAdvanceSalary>();
            TblElectricalSafeties = new HashSet<TblElectricalSafety>();
            TblNightShifts = new HashSet<TblNightShift>();
            TblOvertimes = new HashSet<TblOvertime>();
            TblPhoneInfos = new HashSet<TblPhoneInfo>();
            TblTimeKeepings = new HashSet<TblTimeKeeping>();
            TblUserDepartments = new HashSet<TblUserDepartment>();
            TblUserPositions = new HashSet<TblUserPosition>();
            TblUserRoles = new HashSet<TblUserRole>();
            TblWorkPerformances = new HashSet<TblWorkPerformance>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string UserCode { get; set; }
        public DateTime? Dob { get; set; }
        public string BankNo { get; set; }
        public decimal? SalaryConfficient { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime? DateIssued { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public string Description { get; set; }
        public int? OrderNumber { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public virtual ICollection<TblAdvanceSalary> TblAdvanceSalaries { get; set; }
        public virtual ICollection<TblElectricalSafety> TblElectricalSafeties { get; set; }
        public virtual ICollection<TblNightShift> TblNightShifts { get; set; }
        public virtual ICollection<TblOvertime> TblOvertimes { get; set; }
        public virtual ICollection<TblPhoneInfo> TblPhoneInfos { get; set; }
        public virtual ICollection<TblTimeKeeping> TblTimeKeepings { get; set; }
        public virtual ICollection<TblUserDepartment> TblUserDepartments { get; set; }
        public virtual ICollection<TblUserPosition> TblUserPositions { get; set; }
        public virtual ICollection<TblUserRole> TblUserRoles { get; set; }
        public virtual ICollection<TblWorkPerformance> TblWorkPerformances { get; set; }
    }
}
