using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblWorkPerformance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public decimal? Kti { get; set; }
        public decimal? Kdhi { get; set; }
        public decimal? Kncdi { get; set; }
        public decimal? Nti { get; set; }
        public decimal? Ndhi { get; set; }
        public decimal? Nncdi { get; set; }
        public int? Nle { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public virtual TblUser User { get; set; }
    }
}
