using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblUserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public virtual TblRole Role { get; set; }
        public virtual TblUser User { get; set; }
    }
}
