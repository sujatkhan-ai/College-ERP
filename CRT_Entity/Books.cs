using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CRT_Entity
{
    public class Books
    {
        [Key]
        public int bookid { get; set; }
        public string bookname { get; set; }
        public string bfile { get; set; }
        }
}
