using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using YHW.Models;

namespace YHW.Controllers
{
    [Authorize(Roles = "Editor")]
    public class EditorController : Controller
    {
        //
        // GET: /Editor/

        public ActionResult Index()
        {
            using (var context = new SocialContext())
            {
                var results = new List<YHW.Models.Content.ITopicContent>();
                results.AddRange(context.BlogPost.Include(b => b.Author).ToArray());
                results.AddRange(context.QuotePost.Include(q => q.Author).ToArray());
                results.AddRange(context.VideoPost.Include(v => v.Author).ToArray());

                return View(results.OrderByDescending(t => t.CreatedDate).ToList());
            }
        }
    }
}
