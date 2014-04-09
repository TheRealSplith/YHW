using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHW.Models.SharedViews;
using YHW.Models.Content;
using YHW.Models;

namespace YHW.Controllers
{
    public class TopicController : Controller
    {
        //
        // GET: /Topics/

        public ActionResult Index()
        {
            var model = TopicFilter();
            return View(model);
        }

        [HttpPost]
        public JsonResult RequestData(String everything)
        {
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TopicSideBarCBSection>>(everything);

            IList<String> TopicResults = new List<String>();
            foreach(var section in result)
            {
                var isOpinion = section.Header == "Opinion";
                foreach(var item in section.ChildItems.Where(c => c.IsActive))
                {
                    using (var context = new SocialContext())
                    {
                        switch (item.Label)
                        {
                            case "Blog":
                                var blogPosts = context.BlogPost
                                    .Where(b => b.IsOpinion == isOpinion);
                                foreach(var res in blogPosts)
                                    TopicResults.Add
                                    (
                                        Newtonsoft.Json.JsonConvert.SerializeObject
                                        (
                                            new { Type = "blog", Data = res }
                                        )
                                    );
                                break;
                            case "Quote":
                                var quotePosts = context.QuotePost
                                    .Where(q => q.IsOpinion == isOpinion);
                                foreach (var res in quotePosts)
                                    TopicResults.Add
                                    (
                                        Newtonsoft.Json.JsonConvert.SerializeObject
                                        (
                                            new { Type = "quote", Data = res }
                                        )
                                    );
                                break;
                            case "Video":
                                var videoPosts = context.VideoPost
                                    .Where(p => p.IsOpinion == isOpinion);
                                foreach(var res in videoPosts)
                                    TopicResults.Add
                                    (
                                        Newtonsoft.Json.JsonConvert.SerializeObject
                                        (
                                            new { Type = "video", Data = res }
                                        )
                                    );
                                break;
                        };
                    }
                }
            }
            return Json(TopicResults.ToArray());
        }

        public static TopicSideBar TopicFilter()
        {
            var data = new TopicSideBar();
            data.CBSections.Add(
                new TopicSideBarCBSection
                {
                    Header = "Opinion",
                    ChildItems =
                    new List<TopicSideBarCBItem> 
                    {
                        new TopicSideBarCBItem{ Label = "Blog"},
                        new TopicSideBarCBItem{ Label = "Quote"},
                        new TopicSideBarCBItem{ Label = "Video"},
                        new TopicSideBarCBItem{ Label = "Statistics"}
                    }
                }
            );
            data.CBSections.Add(
                new TopicSideBarCBSection
                {
                    Header = "Fact",
                    ChildItems =
                    new List<TopicSideBarCBItem>
                    {
                        new TopicSideBarCBItem{ Label = "Blog"},
                        new TopicSideBarCBItem{ Label = "Quote"},
                        new TopicSideBarCBItem{ Label = "Video"},
                        new TopicSideBarCBItem{ Label = "Statistics"}
                    }
                }
            );
            return data;
        }
    }
}
