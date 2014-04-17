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
        public JsonResult RequestData(String paramJson)
        {
            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<TopicParams>(paramJson);
            // Add one day to end date so it will include that day
            result.EndDate = result.EndDate.Value.AddDays(1);

            IList<ITopicContent> TopicResults = new List<ITopicContent>();
            List<string> JsonResult = new List<string>();
            foreach(var section in result.CheckBoxSections)
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
                                    .Where(b => b.IsOpinion == isOpinion && b.CreatedDate > result.StartDate && b.CreatedDate < result.EndDate);
                                foreach (var res in blogPosts)
                                    TopicResults.Add(res);
                                break;
                            case "Quote":
                                var quotePosts = context.QuotePost
                                    .Where(q => q.IsOpinion == isOpinion && q.CreatedDate > result.StartDate && q.CreatedDate < result.EndDate);
                                foreach (var res in quotePosts)
                                    TopicResults.Add(res);
                                break;
                            case "Video":
                                var videoPosts = context.VideoPost
                                    .Where(p => p.IsOpinion == isOpinion && p.CreatedDate > result.StartDate && p.CreatedDate < result.EndDate);
                                foreach (var res in videoPosts)
                                    TopicResults.Add(res);
                                break;
                        };
                    }
                }
            }

            foreach(var item in TopicResults.OrderByDescending(i => i.CreatedDate))
            {
                JsonResult.Add(
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        new { Type = item.TypeName.ToLower(), IsOpinion = item.IsOpinion, Data = item }
                        ));
            }

            return Json(JsonResult.ToArray());
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
