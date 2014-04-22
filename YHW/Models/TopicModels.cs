using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHW.Models
{
    public class TopicParams
    {
        public IList<String> Topics { get; set; }
        public IList<TopicSideBarCBSection> CheckBoxSections { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class TopicSideBar
    {
        public TopicSideBar()
        {
            CBSections = new List<TopicSideBarCBSection>();
        }
        public IList<TopicSideBarCBSection> CBSections { get; set; }
    }

    public class TopicSideBarCBSection
    {
        public TopicSideBarCBSection()
        {
            ChildItems = new List<TopicSideBarCBItem>();
        }
        public String Header { get; set; }
        public IList<TopicSideBarCBItem> ChildItems { get; set; }
    }

    public class TopicSideBarCBItem
    {
        public TopicSideBarCBItem()
        {
            IsActive = true;
        }
        public String Label { get; set; }
        public Boolean IsActive { get; set; }
    }
}