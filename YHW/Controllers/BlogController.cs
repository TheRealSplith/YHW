using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHW.Models.Content;
using YHW.Models;

namespace YHW.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Item(int id = -1)
        {
            using (var context = new SocialContext())
            {
                var result = context.BlogPost.Where(b => b.ID == id).FirstOrDefault();
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

        [Authorize]
        [HttpPost]
        public JsonResult New(Blog b)
        {
            using (var context = new SocialContext())
            {
                // calculate SubText
                int textLength = Math.Min(100, b.BlogText.Length);
                b.SubText = b.BlogText.Substring(0, textLength);
                if (textLength != b.BlogText.Length)
                    b.SubText += "...";

                b.Author = context.UserProfile.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                if (b.Author == null)
                    return Json(new { Success = false, Error = "User not authenticated, try logging in again!" });

                b.CreatedDate = DateTime.Now;

                context.BlogPost.Add(b);
                context.SaveChanges();
                return Json(new { Success = true, Redirect = Url.Action("Success", "Blog") });
            }
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
