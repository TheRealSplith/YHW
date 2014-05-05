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
                    // Ensure that call is authenticated
                    if ( result.IsApproved ||
                         Request.IsAuthenticated && HttpContext.User.Identity.Name == result.Author.UserName
                         || User.IsInRole("Editor"))
                        return View(result);
                    else
                        throw new HttpException(500, "Content not available right now, awaiting approval");
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

        [Authorize]
        [HttpPost]
        public ActionResult New(VideoVM vm)
        {
            Video v = new Video();
            using (var context = new SocialContext())
            {
                v.Title = vm.Title;
                v.SubText = vm.SubText;
                // Pull out the Video ID and add the embed code
                if (System.Text.RegularExpressions.Regex.IsMatch(vm.YouTubeURL, @"http[s]?://(www\.)?youtube.*watch\?v=([a-zA-Z0-9\-_]+)"))
                {
                    // after the v parameter is the YTID
                    int vIndex = vm.YouTubeURL.IndexOf("v=");
                    // We offset the index by 2 so that we don't have the v= at the beginning
                    String ytID = vm.YouTubeURL.Substring(vIndex + 2);
                    if (ytID.Contains('&'))
                    {
                        int ampIndex = ytID.IndexOf('&');
                        ytID = ytID.Substring(0, ampIndex);
                    }
                    v.VideoURL = ytID;
                }
                else
                {
                    throw new HttpException(500, "YouTubeURL Parameter is invalid");
                }
                v.Author = context.UserProfile.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
                if (v.Author == null)
                    return RedirectToAction("HOme", "Index");

                v.CreatedDate = DateTime.Now;
                v.IsOpinion = vm.IsOpinion == "on";

                if (User.IsInRole("Writer"))
                    v.IsApproved = true;
                else
                    v.IsApproved = false;

                context.VideoPost.Add(v);
                context.SaveChanges();
                return RedirectToAction("Item", "Video", new { id = v.ID });
            }

        }
        
        public class VideoVM
        {
            public String Title { get; set; }
            public String SubText { get; set; }
            public String IsOpinion { get; set; }
            public String YouTubeURL { get; set; }
        }

    }
}
