using System;
using System.Collections.Generic;
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
    }
}