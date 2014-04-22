using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YHW.Models
{
    public class TopicParams
    {
        String ActionID { get; set; }
        String HeaderName { get; set; }
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
        public TopicSideBarCBSection(String header, String action, Boolean isActive)
        {
            ChildItems = new List<TopicSideBarCBItem>();
            this.Header = header;
            this.Action = action;
            this.IsActive = isActive;
        }
        public String Header { get; set; }
        public String Action { get; set; }
        public Boolean IsActive { get; set; }
        public IList<TopicSideBarCBItem> ChildItems { get; set; }
    }

    public class TopicSideBarCBItem
    {
        public TopicSideBarCBItem(String label, String action, String headerName, Boolean isActive)
        {
            Label = label;
            Action = action;
            HeaderName = headerName;
            IsActive = isActive;
        }
        public String Label { get; private set; }
        public String Action { get; private set; }
        public String HeaderName { get; protected set; }
        public Boolean IsActive { get; private set; }
    }
}