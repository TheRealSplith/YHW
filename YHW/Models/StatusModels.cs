using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHW.Models
{
    public class HttpError
    {
        public Int32 ID { get; set; }
        public Int32 HttpCode { get; set; }
        public String Message { get; set; }
    }
}