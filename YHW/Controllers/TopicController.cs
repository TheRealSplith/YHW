using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using YHW.Models.SharedViews;
using YHW.Models.Content;
using YHW.Models;

namespace YHW.Controllers
{
    public class TopicController : Controller
    {
        //
        // GET: /Topics/

        #region "Entry Requests"
        public ActionResult Index()
        {
            var vm = new IndexVM("", "");
            return View(vm);
        }

        public ActionResult Fact(String id = "")
        {
            var vm = new IndexVM("fact", id);
            return View("Index", vm);
        }

        public ActionResult Opinion(String id = "")
        {
            var vm = new IndexVM("opinion", id);
            return View("Index", vm);
        }
        #endregion

        #region "View Models"
        public class IndexVM
        {
            public IndexVM(String header, String content)
            {
                Header = header;
                Content = content;
            }
            public String Header { get; set; }
            public String Content { get; set; }
        }
        #endregion

        #region "Content Views"
        public PartialViewResult Generic(String header)
        {
            if (header == "")
            {
                using (var context = new SocialContext())
                {
                    List<YHW.Models.Content.ITopicContent> results = new List<ITopicContent>();

                    var items = (from v in context.VideoPost
                                 where v.IsApproved == true
                                 select new { v.ID, ContentType = "Video", v.CreatedDate })
                                     .Union(
                                 from b in context.BlogPost
                                 where v.IsApproved == true
                                 select new { b.ID, ContentType = "Blog", b.CreatedDate })
                                     .Union(
                                 from q in context.QuotePost
                                 where v.IsApproved == true
                                 select new { q.ID, ContentType = "Quote", q.CreatedDate }).ToList();

                    foreach (var item in items)
                    {
                        switch (item.ContentType)
                        {
                            case "Video":
                                var vid = context.VideoPost
                                    .Include(v => v.Author)
                                    .Where(v => v.ID == item.ID).FirstOrDefault();
                                if (vid != null)
                                    results.Add(vid);
                                break;
                            case "Quote":
                                var quote = context.QuotePost
                                    .Include(q => q.Author)
                                    .Where(q => q.ID == item.ID).FirstOrDefault();
                                if (quote != null)
                                    results.Add(quote);
                                break;
                            case "Blog":
                                var blog = context.BlogPost
                                    .Include(b => b.Author)
                                    .Where(b => b.ID == item.ID).FirstOrDefault();
                                if (blog != null)
                                    results.Add(blog);
                                break;
                        }
                    }

                    return PartialView("DualView", results);
                }
            }
            Boolean isOpinion = header == "opinion";
            using (var context = new SocialContext())
            {
                List<YHW.Models.Content.ITopicContent> results = new List<ITopicContent>();

                var items = (from v in context.VideoPost
                             where v.IsOpinion == isOpinion
                                && v.IsApproved == true
                             select new { v.ID, ContentType = "Video", v.CreatedDate })
                                 .Union(
                             from b in context.BlogPost
                             where b.IsOpinion == isOpinion
                                && b.IsApproved == true
                             select new { b.ID, ContentType = "Blog", b.CreatedDate })
                                 .Union(
                             from q in context.QuotePost
                             where q.IsOpinion == isOpinion
                                && q.IsApproved == true
                             select new { q.ID, ContentType = "Quote", q.CreatedDate }).ToList();

                foreach(var item in items)
                {
                    switch (item.ContentType)
                    {
                        case "Video":
                            var vid = context.VideoPost
                                .Include(v => v.Author)
                                .Where(v => v.ID == item.ID).FirstOrDefault();
                            if (vid != null)
                                results.Add(vid);
                            break;
                        case "Quote":
                            var quote = context.QuotePost
                                .Include(q => q.Author)
                                .Where(q => q.ID == item.ID).FirstOrDefault();
                            if (quote != null)
                                results.Add(quote);
                            break;
                        case "Blog":
                            var blog = context.BlogPost
                                .Include(b => b.Author)
                                .Where(b => b.ID == item.ID).FirstOrDefault();
                            if (blog != null)
                                results.Add(blog);
                            break;
                    }
                }
                                 

                ViewBag.isOpinion = isOpinion;
                return PartialView(results);
            }
        }
        public PartialViewResult Video(String header)
        {
            Boolean isOpinion = header == "opinion";
            using (var context = new SocialContext())
            {
                var videos = context.VideoPost
                    .Include(v => v.Author)
                    .Where(v => v.IsOpinion == isOpinion);
                ViewBag.isOpinion = isOpinion;
                return PartialView(videos.ToList());
            }
        }

        public PartialViewResult Quote(String header)
        {
            Boolean isOpinion = header == "opinion";
            using (var context = new SocialContext())
            {
                var quotes = context.QuotePost
                    .Include(v => v.Author)
                    .Where(v => v.IsOpinion == isOpinion);
                ViewBag.isOpinion = isOpinion;
                return PartialView(quotes.ToList());
            }
        }

        public PartialViewResult Blog(String header)
        {
            Boolean isOpinion = header == "opinion";
            using (var context = new SocialContext())
            {
                var blogs = context.BlogPost
                    .Include(v => v.Author)
                    .Where(v => v.IsOpinion == isOpinion);
                ViewBag.isOpinion = isOpinion;
                return PartialView(blogs.ToList());
            }
        }
        #endregion

        #region "Helper Methods"
        public PartialViewResult TopicFilter(String header = "", String content = "")
        {
            var isOpinion = header == "opinion";
            var isFact = header == "fact";

            var data = new TopicSideBar();
            data.CBSections.Add(new TopicSideBarCBSection("Common Core", "", header == ""));
            data.CBSections.Add(
                new TopicSideBarCBSection("Fact", "fact", isFact)
                {
                    Header = "Fact",
                    ChildItems =
                    new List<TopicSideBarCBItem>
                    {
                        new TopicSideBarCBItem("Article", "blog", "Fact", content == "blog" && isFact),
                        new TopicSideBarCBItem("Quote", "quote", "Fact", content == "quote" && isFact),
                        new TopicSideBarCBItem("Video", "video", "Fact", content == "video" && isFact),
                    }
                }
            );
            data.CBSections.Add(
                new TopicSideBarCBSection("Opinion", "opinion", isOpinion)
                {
                    ChildItems =
                    new List<TopicSideBarCBItem> 
                    {
                        new TopicSideBarCBItem("Article", "blog", "Opinion", content == "blog" && isOpinion),
                        new TopicSideBarCBItem("Quote", "quote", "Opinion", content == "quote" && isOpinion),
                        new TopicSideBarCBItem("Video", "video", "Opinion", content == "video" && isOpinion),
                    }
                }
            );
            return PartialView(data);
        }
        #endregion
    }
}
