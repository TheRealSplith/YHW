using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHW.Models.SharedViews;

namespace YHW.Controllers
{
    public class HomeController : Controller
    {
        #region "Actions"
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
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
