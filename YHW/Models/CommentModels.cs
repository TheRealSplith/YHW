using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHW.Models
{
    public class Comment
    {
        public Int64 ID { get; set; }
        public String ContentKey { get; set; }
        public String Message { get; set; }
        public Int32 AuthorID { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual YHWProfile MyAuthor { get; set; }
        public virtual List<Rating> Ratings { get; set; }
    }

    public class Rating
    {
        public Int64 ID { get; set; }
        public Int64 CommentID { get; set; }
        public virtual Comment MyComment { get; set; }
        public Int32 AuthorID { get; set; }
        public virtual YHWProfile MyAuthor { get; set; }
        public Boolean IsPositive { get; set; }
    }
}