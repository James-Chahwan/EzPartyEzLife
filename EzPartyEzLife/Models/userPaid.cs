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
    public class userPaid
    {
        [Key]
        public int id { get; set; }
        public string FBID { get; set; }
        public bool haspaid { get; set; }
        public decimal ammount { get; set; }
        public int partyID { get; set; }
        [ForeignKey("partyID")]
        public virtual party party { get; set; }

    }
}