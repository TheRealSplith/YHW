using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using YHW.Models;

namespace YHW.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Comment/

        [HttpDelete]
        public ActionResult Delete(Int32 id)
        {
            using (var context = new SocialContext())
            {
                YHWProfile currentUser = context.UserProfile.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
                var target = context.ContentComment.Where(cc => cc.ID == id).FirstOrDefault();
                String ContentKey = target.ContentKey;
                if (target.MyAuthor.ID == currentUser.ID)
                    context.ContentComment.Remove(target);

                context.SaveChanges();

                return CommentPartial(ContentKey);
            }
        }

        [HttpPost]
        public ActionResult FormPost(String Message, String ContentKey)
        {
            using (var context = new SocialContext())
            {
                YHWProfile currentUser = context.UserProfile.Where(p => p.UserName == User.Identity.Name).FirstOrDefault();
                Comment newComm = new Comment() {
                    MyAuthor = currentUser,
                    Message = Message,
                    CreationDate = DateTime.Now,
                    ContentKey = ContentKey
                };

                context.ContentComment.Add(newComm);
                context.SaveChanges();

                return CommentPartial(ContentKey);
            }
        }

        public PartialViewResult CommentPartial(String id)
        {
            if (id == null)
                throw new ArgumentException("id cannot be null");

            using (var context = new SocialContext())
            {
                var results = context.ContentComment
                    .Include(cc => cc.MyAuthor)
                    .Include(cc => cc.Ratings)
                    .Where(cc => cc.ContentKey == id)
                    .OrderByDescending(cc => cc.CreationDate);

                CommentPartialVM vm = new CommentPartialVM {
                    Comments = results.ToList(),
                    ContentKey = id
                };

                // I guess it uses the request to figure out which view to use
                // and since I call this from different actions we need to specify
                // to use the view specific to this action
                return PartialView("CommentPartial",vm);
            }
        }

        public class CommentPartialVM
        {
            public List<Comment> Comments {get; set;}
            public String ContentKey {get; set;}
        }

    }
}
