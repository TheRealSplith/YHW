﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YHW.Models.Content
{
    public class Video : ITopicContent
    {
        public Int32 ID { get; set; }
        public String Title { get; set; }
        public String SubText { get; set; }
        public String ImageURL { get; set; }
        public String ThumbURL { get; set; }
        public String VideoURL { get; set; }
        public Boolean IsOpinion { get; set; }
        public DateTime CreatedDate { get; set; }
        public Int32? AuthorID { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public virtual YHWProfile Author { get; set; }
        [NotMapped]
        public String TypeName 
        {
            get { return "Video"; }
            set { throw new NotImplementedException(); }
        }
    }
}