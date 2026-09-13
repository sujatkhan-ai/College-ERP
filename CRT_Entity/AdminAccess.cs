using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRT_Entity
{
    public class AdminAccess
    {
        [Key]
        public int UserRId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ? CreatedDate { get; set; }
        public int Rollid { get; set; }
        public bool IsAccount { get; set; }
    }
}
