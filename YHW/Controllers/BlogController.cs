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

        [Authorize(Roles = "Editor")]
        public ActionResult Approve(Int32 id, Int32 val)
        {
            using (var context = new SocialContext())
            {
                Blog blog = context.BlogPost.Where(b => b.ID == id).Single();
                if (val == 0 || val == 1)
                    blog.IsApproved = val == 1;
                else if (val == 2)
                    context.BlogPost.Remove(blog);
                else
                    throw new HttpException(400, "Bad Request");

                context.SaveChanges();
                return RedirectToAction("Index", "Editor");
            }
        }

        public FileContentResult LargeImage(int id = -1)
        {
            if (id == -1)
                throw new HttpException(404, "Item not found");

            using (var context = new SocialContext())
            {
                Blog blog = context.BlogPost.Where(b => b.ID == id).FirstOrDefault();
                if (blog == null)
                    throw new HttpException(404, "Item not found");

                if (blog.LargeImage == null)
                    throw new HttpException(404, "Image not found");

                return new FileContentResult(blog.LargeImage, "image/jpg");
            }
        }

        public FileContentResult SmallImage(int id = -1)
        {
            if (id == -1)
                throw new HttpException(404, "Item not found");

            using (var context = new SocialContext())
            {
                Blog blog = context.BlogPost.Where(b => b.ID == id).FirstOrDefault();
                if (blog == null)
                    throw new HttpException(404, "Item not found");

                if (blog.SmallImage == null)
                    throw new HttpException(404, "Image not found");

                return new FileContentResult(blog.SmallImage, "image/jpg");
            }
        }

        public FileContentResult FBImage(int id = -1)
        {
            if (id == -1)
                throw new HttpException(404, "Item not found");

            using (var context = new SocialContext())
            {
                Blog blog = context.BlogPost.Where(b => b.ID == id).FirstOrDefault();
                if (blog == null)
                    throw new HttpException(404, "Item not found");

                if (blog.SmallImage == null)
                    throw new HttpException(404, "Image not found");

                return new FileContentResult(blog.FBImage, "image/jpg");
            }
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
                b.ImageSubText = vm.ImageSubText;
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
                // LargeImage
                if (vm.LargeFile != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        vm.LargeFile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();

                        b.LargeImage = array;
                    }
                }
                // SmallImage
                if (vm.SmallFile != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        vm.SmallFile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();

                        b.SmallImage = array;
                    }
                }
                // FBImage
                if (vm.FBFile != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        vm.FBFile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();

                        b.FBImage = array;
                    }
                }

                if (User.IsInRole("Editor") || User.IsInRole("Writer"))
                    b.IsApproved = true;
                else
                    b.IsApproved = false;

                context.BlogPost.Add(b);
                context.SaveChanges();
                return RedirectToAction("Item", "Blog", new { id = b.ID });
            }
        }

        public class NewBlogVM
        {
            public HttpPostedFileBase ThumbFile { get; set; }
            public HttpPostedFileBase LargeFile { get; set; }
            public HttpPostedFileBase SmallFile { get; set; }
            public HttpPostedFileBase FBFile { get; set; }
            public String IsOpinion { get; set; }
            public String BlogText { get; set; }
            public String Title { get; set; }
            public String ImageSubText { get; set; }
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}
