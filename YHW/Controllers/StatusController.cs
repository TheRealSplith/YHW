using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHW.Models.Content;
using YHW.Models;

namespace YHW.Controllers
{
    public class StatusController : Controller
    {
        //
        // GET: /Status/

        public ActionResult Http404(String s = "")
        {
            using (var context = new SocialContext())
            {
                context.Errors.Add(new HttpError
                    {
                        ID = -1,
                        HttpCode = 404,
                        Message = String.Format("HTTP404:{0}",s)
                    });
                context.SaveChanges();
            }
            return View();
        }

    }
}
