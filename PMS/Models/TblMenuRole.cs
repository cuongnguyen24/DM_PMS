using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblMenuRole
    {
        public int Id { get; set; }
        public int MenuId { get; set; }
        public int RoleId { get; set; }

        public virtual TblMenu Menu { get; set; }
        public virtual TblRole Role { get; set; }
    }
}
