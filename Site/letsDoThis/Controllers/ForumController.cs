using letsDoThis.Management;
using letsDoThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace letsDoThis.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        PostManagement pm = new PostManagement();
        public ActionResult ForumList()
        {
            
            return View(pm.ListPost().OrderByDescending(x=>x.ModifiedDate).ToList());
        }

        public ActionResult GoToPost(int? id)
        {
            Post post = pm.FindPost(id);
            TempData["thepost"] = post;
            return View(post);
        }
        [HttpPost]
        public ActionResult YorumEkle(string GelenText)
        {
            User user = Session["login"] as User;
            Post post = TempData["thepost"] as Post;
            Post thepost = pm.FindPost(post.PostID);
            Comments comment = new Comments();
            if (GelenText != null)
            {
                comment.Owner = user;
                comment.CommentText = GelenText;
                comment.thePost = thepost;
                int x = pm.AddComment(comment);
                if (x > 0)
                {
                    return Json(JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(JsonRequestBehavior.DenyGet);
                }
            }
            return Json(JsonRequestBehavior.DenyGet);
        }
        public ActionResult Edit(int? id)
        {
            Post post = pm.FindPost(id);

            return View(post);
        }
        [HttpPost]
        public ActionResult Edit(Post post)
        {

            int x = pm.UpdatePost(post);
            if (x > 0)
            {
                return View(post);
            }
            else
            {
                return View(post);
            }
        }

        public ActionResult Delete(int? id)
        {
            Post post = pm.FindPost(id);
            int x = pm.DeletePost(post);
            if (x > 0)
            {
                return RedirectToAction("ForumList");
            }
            else
            {
                return RedirectToAction("ForumList");
            }
        }
        
        public ActionResult PostEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PostEkle(Post post)
        {
            if (post != null)
            {
                User user = Session["login"] as User;
                post.owner = user;
                int x = pm.AddPost(post);
                if (x == 0)
                {
                    return View(post);
                }
                else
                {
                    return RedirectToAction("ForumList");
                }

            }
            else
            {
                return View();
            }

        }
        [HttpPost]
        public ActionResult YorumSil(int id)
        {
            int x = pm.DeleteComment(id);
            if (x > 0)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonRequestBehavior.DenyGet);
            }
        }
        
        [HttpPost]
        public ActionResult Like(int id)
        {
            int x = pm.Like(pm.FindPost(id));
            if (x > 0)
            {
                return Json(JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(JsonRequestBehavior.DenyGet);
            }

        }

    }
}