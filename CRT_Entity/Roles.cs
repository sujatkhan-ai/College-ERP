using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRT_Entity
{
    public class Roles
    {
        [Key]
        public int Rollid { get; set; }
        public string Rolltitle { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ? CreatedDate { get; set; } 
    }
}
