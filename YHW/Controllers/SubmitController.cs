using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YHW.Controllers
{
    public class SubmitController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
