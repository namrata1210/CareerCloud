using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Education")]
    class CompanyJobEducationPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Job { get; set; }
        public string Major { get; set; }
        public Int16? Importance { get; set; }
        [Column("Time_stamp")]
        public byte[] TimeStamp { get; set; }
    }
}
