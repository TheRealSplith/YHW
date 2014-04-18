using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YHW.Models
{
    public class TeamProfile
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public String Title { get; set; }
        public String PortraitURL { get; set; }
        public String FacebookLink { get; set; }
        public String TwitterLink { get; set; }
        public String LinkedIn { get; set; }
        public Boolean YHWTeam { get; set; }
    }

    public class YHWProfile
    {
        public Int32 ID { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        public Boolean? IsMale { get; set; }
        public String PortraitURL { get; set; }
        public String FacebookLink { get; set; }
        public String TwitterLink { get; set; }
        public String LinkedIn { get; set; }
        public String Title { get; set; }
    }
}