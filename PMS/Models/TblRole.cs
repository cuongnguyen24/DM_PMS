using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblRole
    {
        public TblRole()
        {
            TblMenuRoles = new HashSet<TblMenuRole>();
            TblUserRoles = new HashSet<TblUserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<TblMenuRole> TblMenuRoles { get; set; }
        public virtual ICollection<TblUserRole> TblUserRoles { get; set; }
    }
}
