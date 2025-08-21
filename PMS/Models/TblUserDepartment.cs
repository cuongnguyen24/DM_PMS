using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblUserDepartment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime DateIssued { get; set; }
        public int Status { get; set; }
        public int? OrderNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public virtual TblDepartment Department { get; set; }
        public virtual TblUser User { get; set; }
    }
}
