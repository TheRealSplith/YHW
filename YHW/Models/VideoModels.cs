using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHW.Models
{
    public class Video
    {
        public Int32 ID { get; set; }
        public String Title { get; set; }
        public String VideoURL { get; set; }
        public Boolean IsOpinion { get; set; }
        public DateTime CreateDate { get; set; }
    }
}