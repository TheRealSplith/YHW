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
                    // Ensure that call is authenticated
                    if (Request.IsAuthenticated && HttpContext.User.Identity.Name == result.Author.UserName || User.IsInRole("Editor"))
                        return View(result);
            }
        }

        [Authorize(Roles = "Editor")]
        public ActionResult Approve(Int32 id, Int32 val)
        {
            using (var context = new SocialContext())
            {
                Quote quote = context.QuotePost.Where(q => q.ID == id).Single();
                if (val == 0 || val == 1)
                    quote.IsApproved = val == 1;
                else if (val == 2)
                    context.QuotePost.Remove(quote);
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

                        q.ThumbImage = array;
                    }
                }
                // Image
                if (vm.ImageFile != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        vm.ImageFile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();

                        q.LargeImage = array;
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
