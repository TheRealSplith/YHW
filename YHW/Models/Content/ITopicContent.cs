﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHW.Models.Content
{
    public interface ITopicContent
    {
        Int32 ID { get; set; }
        Byte[] ThumbURL { get; set; }
        Byte[] ImageURL { get; set; }
        String Title { get; set; }
        String SubText { get; set; }
        DateTime CreatedDate { get; set; }
        Int32? AuthorID { get; set; }
        String TypeName { get; set; }
        Boolean IsOpinion { get; set; }
    }
}
