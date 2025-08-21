using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblTimeKeeping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? Lv { get; set; }
        public decimal? H { get; set; }
        public decimal? P { get; set; }
        public decimal? L { get; set; }
        public decimal? Ots { get; set; }
        public decimal? Cd { get; set; }
        public decimal? Kl { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public virtual TblUser User { get; set; }
    }
}
