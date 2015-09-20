using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ezpartyezLife.Models
{
    public class party
    {
        [Key]
        public int id { get; set; }
        public string AdminID { get; set; }
        public string eventID { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public decimal totalcost { get; set; }
        

    }
}