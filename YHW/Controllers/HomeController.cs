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
    public class HomeController : Controller
    {
        #region "Actions"
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            List<ITopicContent> viewModel = new List<ITopicContent>();

            using (var content = new SocialContext())
            {
                var results = (from b in content.BlogPost
                               select new
                                   {
                                       ID = b.ID,
                                       Type = "Blog",
                                       CreatedDate = b.CreatedDate
                                   })
                              .Concat(
                                from q in content.QuotePost
                                select new
                                    {
                                        ID = q.ID,
                                        Type = "Quote",
                                        CreatedDate = q.CreatedDate
                                    })
                              .Concat(
                                from v in content.VideoPost
                                select new
                                    {
                                        ID = v.ID,
                                        Type = "Video",
                                        CreatedDate = v.CreatedDate
                                    }
                              ).ToList();
                int count = Math.Min(5, results.Count());
                var dateSortedResults = results.OrderByDescending(a => a.CreatedDate).Take(count).ToList();
                foreach (var item in results)
                {
                    Console.WriteLine(item);

                    switch (item.Type)
                    {
                        case "Video" :
                            viewModel.Add(content.VideoPost.Where(v => v.ID == item.ID).FirstOrDefault());
                            break;
                        case "Quote" :
                            viewModel.Add(content.QuotePost.Where(q => q.ID == item.ID).FirstOrDefault());
                            break;
                        case "Blog" :
                            viewModel.Add(content.BlogPost.Where(b => b.ID == item.ID).FirstOrDefault());
                            break;
                    }
                }
            }

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Vision()
        {
            return View();
        }

        public ActionResult Team()
        {
            using (var context = new YHW.Models.SocialContext())
            {
                var data = context.YHWTeam.AsEnumerable().ToList();
                return View(data);
            }
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion

        #region "Helpers"
        public static List<TextColumnHeaderData> AboutUsNavData(String active)
        {
            var data = new List<TextColumnHeaderData>();
            data.Add(new TextColumnHeaderData { Label = "Mission", Active = active == "about", Action = "About", Controller = "Home" });
            data.Add(new TextColumnHeaderData { Label = "Vision", Active = active == "vision", Action = "Vision", Controller = "Home" });
            data.Add(new TextColumnHeaderData { Label = "Team", Active = active == "team", Action = "Team", Controller = "Home" });

            return data;
        }
        #endregion

    }
}
