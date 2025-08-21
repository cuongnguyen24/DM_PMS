using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblAdvanceSalary
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? BackCharge { get; set; }
        public decimal? BackPay { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public virtual TblUser User { get; set; }
    }
}
