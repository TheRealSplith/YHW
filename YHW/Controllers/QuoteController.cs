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
        public ActionResult New(NewQuoteVM vm)
        {
            Quote q = new Quote();
            using (var context = new SocialContext())
            {
                q.Author = context.UserProfile.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                if (q.Author == null)
                    RedirectToAction("Home", "Index");

                q.CreatedDate = DateTime.Now;
                q.Title = vm.Title;
                q.SubText = vm.SubText;
                q.IsOpinion = vm.IsOpinion == "on";

                // ThumbImage
                if (vm.ThumbFile != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        vm.ThumbFile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();

                        q.ThumbURL = array;
                    }
                }
                // Image
                if (vm.ImageFile != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        vm.ImageFile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();

                        q.ImageURL = array;
                    }
                }

                context.QuotePost.Add(q);
                context.SaveChanges();
                return RedirectToAction("Item", "Quote", new { id = q.ID });
            }
        }

        public class NewQuoteVM
        {
            public HttpPostedFileBase ThumbFile { get; set; }
            public HttpPostedFileBase ImageFile { get; set; }
            public String IsOpinion { get; set; }
            public String SubText { get; set; }
            public String Title { get; set; }
        }
    }
}
