using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblPhoneLimit
    {
        public int Id { get; set; }
        public int PosionId { get; set; }
        public int ValueLimit { get; set; }
        public DateTime DateIssued { get; set; }
        public string Note { get; set; }
        public int Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedUser { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedUser { get; set; }

        public virtual TblPosition Posion { get; set; }
    }
}
