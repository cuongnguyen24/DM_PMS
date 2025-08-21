using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblElectricalSafety
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? TotalDay { get; set; }
        public decimal? Kcvi { get; set; }
        public decimal? Knqi { get; set; }
        public decimal? Ki { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public virtual TblUser User { get; set; }
    }
}
