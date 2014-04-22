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
            Boolean isOpinion = header == "opinion";
            using (var context = new SocialContext())
            {
                ViewBag.isOpinion = isOpinion;
                return PartialView();
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
            data.CBSections.Add(
                new TopicSideBarCBSection("Opinion", "opinion", isOpinion)
                {
                    ChildItems =
                    new List<TopicSideBarCBItem> 
                    {
                        new TopicSideBarCBItem("Blog", "blog", "Opinion", content == "blog" && isOpinion),
                        new TopicSideBarCBItem("Quote", "quote", "Opinion", content == "quote" && isOpinion),
                        new TopicSideBarCBItem("Video", "video", "Opinion", content == "video" && isOpinion),
                    }
                }
            );
            data.CBSections.Add(
                new TopicSideBarCBSection("Fact", "fact", isFact)
                {
                    Header = "Fact",
                    ChildItems =
                    new List<TopicSideBarCBItem>
                    {
                        new TopicSideBarCBItem("Blog", "blog", "Fact", content == "blog" && isFact),
                        new TopicSideBarCBItem("Quote", "quote", "Fact", content == "quote" && isFact),
                        new TopicSideBarCBItem("Video", "video", "Fact", content == "video" && isFact),
                    }
                }
            );
            return PartialView(data);
        }
        #endregion
    }
}
