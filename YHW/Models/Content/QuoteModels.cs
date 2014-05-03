﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YHW.Models.Content
{
    public class Quote : ITopicContent
    {
        public Int32 ID { get; set; }
        public Byte[] LargeImage { get; set; }
        public Byte[] SmallImage { get; set; }
        public Byte[] ThumbImage { get; set; }
        public Byte[] FBImage { get; set; }
        public String Title { get; set; }
        public String SubText { get; set; }
        public String ImageSubText { get; set; }
        public DateTime CreatedDate { get; set; }
        public Boolean IsApproved { get; set; }
        public String ApprovalMemo { get; set; }
        public DateTime? ApprovalUpdateDate { get; set; }
        public Int32? AuthorID { get; set; }
        public Boolean IsOpinion { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public virtual YHWProfile Author { get; set; }
        [NotMapped]
        public String TypeName
        {
            get { return "Quote"; }
            set { throw new NotImplementedException(); }
        }
    }
}