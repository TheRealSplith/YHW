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
                var result = context.BlogPost
                    .Include(b => b.Author)
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

        [Authorize]
        [HttpPost]
        public ActionResult New(NewBlogVM vm)
        {
            Blog b = new Blog();
            using (var context = new SocialContext())
            {
                b.BlogText = vm.BlogText;
                b.Title = vm.Title;
                // calculate SubText
                int textLength = Math.Min(100, b.BlogText.Length);
                b.SubText = b.BlogText.Substring(0, textLength);
                if (textLength != b.BlogText.Length)
                    b.SubText += "...";

                b.Author = context.UserProfile.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                if (b.Author == null)
                    return RedirectToAction("Home", "Index");

                b.CreatedDate = DateTime.Now;
                b.IsOpinion = vm.IsOpinion == "on";

                // ThumbImage
                if (vm.ThumbFile != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        vm.ThumbFile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();

                        b.ThumbImage = array;
                    }
                }
                // Image
                if (vm.ImageFile != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        vm.ImageFile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();

                        b.LargeImage = array;
                    }
                }

                context.BlogPost.Add(b);
                context.SaveChanges();
                return RedirectToAction("Item", "Blog", new { id = b.ID });
            }
        }

        public class NewBlogVM
        {
            public HttpPostedFileBase ThumbFile { get; set; }
            public HttpPostedFileBase ImageFile { get; set; }
            public String IsOpinion { get; set; }
            public String BlogText { get; set; }
            public String Title { get; set; }
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}
