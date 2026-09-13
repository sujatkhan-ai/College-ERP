using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRT_Entity
{
    public class Employee
    {
        [Key]
        public int empId { get; set; }
        public string empName { get; set; }
        public string empEmail { get; set; }
        public string empGender { get; set; }
        public string empMobileNo { get; set; }
        public string empState { get; set; }
        public string empCity { get; set; }
        public string empEmpTypes { get; set; }
        public string empExpYears { get; set; }
        public string empQualification { get; set; }
        public string empApplyFor { get; set; }
        public string empResume { get; set; } 
        public DateTime empDate { get; set; }
        
    }
}
