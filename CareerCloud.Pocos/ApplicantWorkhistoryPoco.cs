﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.Pocos
{
    [Table("Applicant_Work_History")]
    class ApplicantWorkhistoryPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Applicant { get; set; }
        [Column("Company_Name")]
        public string CompanyName { get; set;}
        [Column("country_code")]
        public string CountryCode { get; set; }
        public string Location { get; set; }
        [Column("Job_Title")]
        public string JobTitle { get; set; }
        [Column("Job_description")]
        public string JobDescription { get; set; }
        [Column("Start_Month")]
        public Int16? StartMonth { get; set; }
        [Column("Start_Year")]
        public Int32? StartYear { get; set; }
        [Column("End_Month")]
        public Int16? EndMonth { get; set; }
        [Column("End_year")]
        public Int32? EndYear { get; set; }
        [Column("Time_Stamp")]
        public byte[] TimeStamp { get; set; }
    }
}
