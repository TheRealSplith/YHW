using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHW.Models.SharedViews
{
    public class TextColumnWithHeaderVM
    {
        public TextColumnWithHeaderVM()
        {
        }
        public String Header {get; set;}
        public List<TextColumnHeaderData> Items { get; set; }
    }

    public class TextColumnHeaderData
    {
        public String Label {get; set;}
        public Boolean Active {get; set;}
        public String Action {get; set;}
        public String Controller {get; set;}
    }
}