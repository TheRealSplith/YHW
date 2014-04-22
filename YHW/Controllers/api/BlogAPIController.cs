using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YHW.Models.Content;

namespace YHW.Controllers.api
{
    public class BlogAPIController : ApiController
    {
        [HttpGet]
        public Blog Get(int id = -1)
        {
            if (id == -1)
                return null;
            else
                using (var context = new YHW.Models.SocialContext())
                {
                    return context.BlogPost.Where(b => b.ID == id).FirstOrDefault();
                }
        }

        [HttpPost]
        public void Post(Blog b)
        {
            if (b == null)
                return;

            using (var context = new YHW.Models.SocialContext())
            {
                try
                {
                    context.BlogPost.Add(b);
                    context.SaveChanges();
                }
                catch(Exception e)
                {
                    // Dive on error
                    Console.WriteLine(e.Message);
                }
            }

                
        }
    }
}
