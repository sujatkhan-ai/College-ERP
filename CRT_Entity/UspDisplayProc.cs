using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace CRT_Entity
{
    [Table("Roles")]
    public class UspDisplayProc
    {
        [Key]
        public int Rollid { get; set; }
        public string Rolltitle { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
