using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRT_Entity
{
    public class JobPost
    {
        [Key] 
        public long jobPid { get; set; }
        public string jobTitle { get; set; }
        public string jobExp { get; set; }
        public string jobLocation { get; set; }
        public string jobType { get; set; }
        public string jobSkills { get; set; }
        public string jobDescription { get; set; }
        public string Remarks { get; set; }
        public DateTime jobDate { get; set; }
        
    }
}
