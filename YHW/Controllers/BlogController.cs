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

        public Blog BlogItem(int id = -1)
        {
            using (var context = new SocialContext())
            {
                var result = context.BlogPost.Where(b => b.ID == id).FirstOrDefault();
                return result;
            }
        }

    }
}
