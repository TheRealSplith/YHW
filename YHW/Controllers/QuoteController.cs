using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using YHW.Models.Content;
using YHW.Models;

namespace YHW.Controllers
{
    public class QuoteController : Controller
    {
        //
        // GET: /Quote/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Item(int id = -1)
        {
            using (var context = new SocialContext())
            {
                var result = context.QuotePost
                    .Include(q => q.Author)
                    .Where(b => b.ID == id).FirstOrDefault();
                if (result == null)
                    return RedirectToAction("Http404", "Status", new { id = id.ToString() });
                else
                    return View(result);
            }
        }

        [Authorize]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult New(Quote q)
        {
            using (var context = new SocialContext())
            {
                q.Author = context.UserProfile.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                if (q.Author == null)
                    return Json(new { Success = false, Error = "User not authenticated, try logging in again!" });

                q.CreatedDate = DateTime.Now;

                context.QuotePost.Add(q);
                context.SaveChanges();
                return Json(new { Success = true, Redirect = Url.Action("Index", "Home") });
            }
        }
    }
}
