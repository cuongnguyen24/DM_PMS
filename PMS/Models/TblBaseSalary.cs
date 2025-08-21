using System;
using System.Collections.Generic;

#nullable disable

namespace PMS.Models
{
    public partial class TblBaseSalary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public DateTime DateIssued { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
