using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHW.Models
{
    public class Quote
    {
        public Int32 ID { get; set; }
        public String ThumbURL { get; set; }
        public String ImageURL { get; set; }
        public String Title { get; set; }
        public String QuoteText { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int32? AuthorID { get; set; }
        public Boolean IsOpinion { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual TeamProfile Author { get; set; }
    }
}