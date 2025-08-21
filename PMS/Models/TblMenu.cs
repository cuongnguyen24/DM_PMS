using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblMenu
    {
        public TblMenu()
        {
            TblMenuRoles = new HashSet<TblMenuRole>();
        }

        public int Id { get; set; }
        public int ParentId { get; set; }
        public int TypeMenu { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public int OrderNumber { get; set; }
        public string IconUrl { get; set; }
        public string MetaTitle { get; set; }
        public int Status { get; set; }

        public virtual ICollection<TblMenuRole> TblMenuRoles { get; set; }
    }
}
