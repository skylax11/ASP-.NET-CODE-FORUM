using letsDoThis.EF;
using letsDoThis.Management;
using letsDoThis.Models;
using letsDoThis.RegisterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace letsDoThis.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            UserManagement am = new UserManagement();
            User login = am.Login(user);
            if (login != null && Session["errors"] == null)
            {
                Session["login"] = login;
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegMod user)
        {
            RepoCommand<User> repoCommand = new RepoCommand<User>();
            UserManagement um = new UserManagement();
            if (user != null)
            {
                User us = um.RegUser(user);
                if (us != null)
                {
                    return RedirectToAction("RegisterOK");
                }
                else
                {
                    return View();  // TO DO: VALIDATION MESSAGE 
                }
            }
            else
            {
                Session["errors"] = "Kullanıcı kayıt olamadı";
                return View();
            }
        }
        public ActionResult RegisterOK()
        {
            return View();
        }
        public ActionResult UserActivate(Guid id)
        {
            UserManagement um = new UserManagement();
            if (id != null)
            {
                int sayi = um.ActivateUser(id);
                if (sayi > 0)
                {
                    return View("UserActivateOK");
                }
            }
            return View();
        }
        public ActionResult UserActivateOK()
        {
            return View();
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgotPassword(User user)
        {
            UserManagement um = new UserManagement();
            User forgottenUser = um.FindUser(user.UserName);
            User forgottenEM = um.FindUserEM(user.email);
            if (forgottenUser != null)
            {
                if (forgottenEM != forgottenUser)
                {
                    um.ForgotPassword(user);
                    return View();
                }
                else
                {
                    um.ForgotPassword(forgottenUser);
                    return View();
                }

            }
            else
            {
                TempData["hata"] = "Kişi bulunamadı.";
                return View();
            }
        }
        public ActionResult ForgetPW()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPW(User user)
        {
            UserManagement um = new UserManagement();
            int x = um.ForgotPasswordOK(user);
            if (x > 0)
            {
                return RedirectToAction("Login");
            }
            else
            {
                TempData["hata2"] = "Bir sorun oluştu. Lütfen tekrar deneyiniz.";
                return View();
            }
        }
        public ActionResult WhatDoWeMean()
        {
            return View();
        }
        public ActionResult EGG()
        {
            return View();
        }

    }
}