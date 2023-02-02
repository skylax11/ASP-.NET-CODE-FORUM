using letsDoThis.Management;
using letsDoThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace letsDoThis.Controllers
{
    public class ProfileController : Controller
    {
        UserManagement um = new UserManagement();
        // GET: Profile
        public ActionResult ShowProfile()
        {
            User user = Session["login"] as User;
            return View(user);
        }
        public ActionResult SetProfile()
        {
            User user = Session["login"] as User;
            return View(user);
        }
        [HttpPost]
        public ActionResult SetProfile(User user, HttpPostedFileBase ProfileImage)
        {
            User currentUser = Session["login"] as User;
            string picture;

            if (user.ProfileImage == null)
            {
                picture = null;
            }
            else
            {
                if (ProfileImage != null)
                {
                    picture = ProfileImage.FileName;

                }
                else
                {
                    picture = currentUser.ProfileImage;
                }
            }
            if (ProfileImage != null &&
            (ProfileImage.ContentType == "image/jpg" ||
            ProfileImage.ContentType == "image/jpeg" ||
            ProfileImage.ContentType == "image/png"))
            {
                string filename = $"user_{user.UserID}.{ProfileImage.ContentType.Split('/')[1]}";
                ProfileImage.SaveAs(Server.MapPath($"~/Img/{filename}"));
                user.ProfileImage = filename;
            }
            else
            {
                if (user.ProfileImage == null)
                {
                    Session["Editerror"] = "Profil fotografı uyumsuz";
                }
            }
            um.UpdateUser(user, picture);
            User updatedUser = Session["login"] as User;
            int? x = Session["deger"] as int?;
            if (x > 0)
            {
                return View(updatedUser);
            }
            else
            {
                Session["login"] = user;
                return View(user);
            }
        }
        public ActionResult DeleteProfile()
        {
            User deleteUser = Session["login"] as User;
            int temp = um.DeleteUser(deleteUser);
            if (temp > 0)
            {
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult DeleteDetectedProfile(int? id)
        {
            return View(um.FindUser(id));
        }
        [HttpPost,ActionName("DeleteDetectedProfile")]
        public ActionResult DeleteDetectedProfil(int id)
        {
            User user = um.FindUser(id);
            int temp = um.DeleteUser(user);
            if (temp > 0)
            {
                return RedirectToAction("ViewAll", "Profile");
            }
            else
            {
                return RedirectToAction("ViewAll", "Profile");
            }
        }
        public ActionResult SetAsAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetAsAdmin(User user)
        {
            User user_ = um.AddUser(user);
            if (user_ != null)
            {
                return RedirectToAction("ViewAll");
            }
            else
            {
                return View();
            }
        }
        public ActionResult ViewAll()
        {

            return View(um.getList());
        }

        public ActionResult EditUser(int id)
        {
            return View(um.FindUser(id));
        }
        [HttpPost]
        public ActionResult EditUser(User newUser)
        {
            User oldUser = um.FindUser(newUser.UserID);
            um.UpdatedByAdmin(newUser,oldUser,oldUser.ProfileImage);
            return View();
        }

    }
}