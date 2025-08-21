using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblPosition
    {
        public TblPosition()
        {
            TblPhoneLimits = new HashSet<TblPhoneLimit>();
            TblUserPositions = new HashSet<TblUserPosition>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int TypeObject { get; set; }
        public decimal? BasicPosiontConfficient { get; set; }
        public decimal? ResponsibilityCoefficient { get; set; }
        public decimal? PosiontConfficient { get; set; }
        public DateTime DateIssued { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }

        public virtual ICollection<TblPhoneLimit> TblPhoneLimits { get; set; }
        public virtual ICollection<TblUserPosition> TblUserPositions { get; set; }
    }
}
