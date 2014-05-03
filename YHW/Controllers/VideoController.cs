using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using YHW.Models;
using YHW.Models.Content;

namespace YHW.Controllers
{
    public class VideoController : Controller
    {
        //
        // GET: /Video/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Item(int id = -1)
        {
            using (var context = new SocialContext())
            {
                var result = context.VideoPost
                    .Include(v => v.Author)
                    .Where(b => b.ID == id).FirstOrDefault();
                if (result == null)
                    return RedirectToAction("Http404", "Status", new { id = id.ToString() });
                else
                    return View(result);
            }
        }

        [Authorize(Roles = "Editor")]
        public ActionResult Approve(Int32 id, Int32 val)
        {
            using (var context = new SocialContext())
            {
                Video vid = context.VideoPost.Where(v => v.ID == id).Single();
                if (val == 0 || val == 1)
                    vid.IsApproved = val == 1;
                else if (val == 2)
                    context.VideoPost.Remove(vid);
                else
                    throw new HttpException(400, "Bad Request");

                context.SaveChanges();
                return RedirectToAction("Index", "Editor");
            }
        }

        [Authorize]
        public ActionResult New()
        {
            return View();
        }

    }
}
