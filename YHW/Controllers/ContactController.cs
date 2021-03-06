﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YHW.Models.SharedViews;
using YHW.Models.Content;
using YHW.Models;

namespace YHW.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/

        public ActionResult Index()
        {
            return View(new Feedback());
        }

        [HttpPost]
        public ActionResult Index(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SocialContext())
                {
                    context.Feedback.Add(feedback);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Collaborate()
        {
            return View(new CollabRequest());
        }

        [HttpPost]
        public ActionResult Collaborate(CollabRequest request)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SocialContext())
                {
                    context.CollabRequest.Add(request);
                    context.SaveChanges();
                }
                return RedirectToAction("Collaborate");
            }
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public static List<TextColumnHeaderData> ContactNavData(String active)
        {
            var data = new List<TextColumnHeaderData>();
            data.Add(new TextColumnHeaderData { Label = "Feedback", Active = active == "contact", Action = "Index", Controller = "Contact" });
            data.Add(new TextColumnHeaderData { Label = "Collaborate", Active = active == "collaborate", Action = "Collaborate", Controller = "Contact" });

            return data;
        }
    }
}
