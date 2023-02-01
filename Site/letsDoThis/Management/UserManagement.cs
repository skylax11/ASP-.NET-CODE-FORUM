using letsDoThis.EF;
using letsDoThis.Mail;
using letsDoThis.Models;
using letsDoThis.RegisterModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace letsDoThis.Management
{
    public class UserManagement
    {
        List<string> Errors = new List<string>();
        RepoCommand<User> command = new RepoCommand<User>();
        RepoCommand<Post> commandPost = new RepoCommand<Post>();
        RepoCommand<Like> commandLike = new RepoCommand<Like>();
        RepoCommand<Comments> commandComments = new RepoCommand<Comments>();

        public List<User> getList()
        {
            return command.List();
        }
        public User Login(User user)
        {
            User db_USERNAME = command.Find(x => x.UserName == user.UserName);
            User db_PASSWORD = command.Find(x => x.Password == user.Password);
            HttpContext.Current.Session["errors"] = null;

            if (db_USERNAME != null && db_PASSWORD != null)
            {
                if (db_USERNAME.Password == db_PASSWORD.Password && db_USERNAME.isActivate == true)
                {
                    return db_USERNAME;

                }
                else if (db_USERNAME.Password == db_PASSWORD.Password && db_USERNAME.isActivate == false)
                {

                    Errors.Add("Aktive edilmemiş hesap.");
                    HttpContext.Current.Session["errors"] = Errors;
                    return null;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                Errors.Add("Giriş bilgileriniz sistemdekiyle uyuşmuyor");
                HttpContext.Current.Session["errors"] = Errors;
                return null;
            }
        }

        public User RegUser(RegMod user)
        {
            HttpContext.Current.Session["errors"] = null;
            if (user != null)
            {
                User username = command.Find(xy => xy.UserName == user.UserName);
                User email = command.Find(y => y.email == user.email);
                if (username != null)
                {
                    Errors.Add("Kayıtlı kullanıcı adı.");
                    HttpContext.Current.Session["Rerrors"] = Errors;
                    return null;
                }
                if (email != null)
                {
                    Errors.Add("Kayıtlı Email.");
                    HttpContext.Current.Session["Rerrors"] = Errors;
                    return null;
                }
                if (user.Password != user.REPassword)
                {
                    Errors.Add("Şifreler uyuşmuyor.");
                    HttpContext.Current.Session["Rerrors"] = Errors;
                    return null;
                }

                int x = command.Insert(new User()
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    ActivateGuid = Guid.NewGuid(),
                    ProfileImage = "common.png",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    isActivate = false,
                    isAdmin = false,
                    email = user.email

                });
                User addedUser = command.Find(xz => xz.UserName == user.UserName);
                if (x > 0)
                {

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/UserActivate/{addedUser.ActivateGuid}";
                    string body = $"Merhaba {addedUser.UserName}! Hesabınızı aktifleştirmek için <a href='{activateUri}'>tıklayınız</a>.";
                    MailHelper.SendMail(body, addedUser.email, "Code Forum hesap aktifleştirme");

                    return addedUser;
                }
                else
                {
                    return null;
                }

            }
            else
            {
                return null;
            }
        }
        public int ActivateUser(Guid id)
        {
            User user = command.Find(y => y.ActivateGuid == id);
            if (user != null)
            {
                user.isActivate = true;
                int isx = command.Save();
                return isx;
            }
            else
            {
                return 0;
            }
        }
        public void UpdatedByAdmin(User newUser, User oldUser, string pic)
        {
            bool girdiMi = false;

            if (newUser != null && oldUser != null)
            {
                User findUserIfExistUSERNAME = command.Find(y => y.UserName == newUser.UserName);
                User findUserIfExistEMAIL = command.Find(y => y.email == newUser.email);
                if (findUserIfExistUSERNAME != null)
                {
                    if (findUserIfExistUSERNAME.UserID != newUser.UserID)
                    {
                        Errors.Add("Kayıtlı kullanıcı adı");
                        HttpContext.Current.Session["EDError"] = Errors;
                        girdiMi = true;
                    }
                }
                else if (findUserIfExistEMAIL != null)
                {
                    if (findUserIfExistEMAIL.UserID != newUser.UserID)
                    {
                        Errors.Add("Kayıtlı eposta");
                        HttpContext.Current.Session["EDError"] = Errors;
                        girdiMi = true;
                    }
                }
            }
            if (girdiMi == false)
            {
                User updateUser = command.Find(xz => xz.UserID == newUser.UserID);

                updateUser.UserName = newUser.UserName;
                updateUser.Name = newUser.Name;
                updateUser.Password = newUser.Password;
                updateUser.email = newUser.email;
                updateUser.Surname = newUser.Surname;
                updateUser.ProfileImage = pic;
                updateUser.isActivate = newUser.isActivate;
                updateUser.isAdmin = newUser.isAdmin;

                command.Save();

            }
        }
        public User AddUser(User user)
        {
            if (user != null)
            {
                User username = command.Find(xy => xy.UserName == user.UserName);
                User email = command.Find(y => y.email == user.email);
                if (username != null)
                {
                    Errors.Add("Kayıtlı kullanıcı adı.");
                    HttpContext.Current.Session["ARerrors"] = Errors;
                    return null;
                }
                if (email != null)
                {
                    Errors.Add("Kayıtlı Email.");
                    HttpContext.Current.Session["ARerrors"] = Errors;
                    return null;
                }
                int x = command.Insert(new User()
                {
                    Name=user.Name, 
                    Surname=user.Surname,
                    UserName = user.UserName,
                    Password = user.Password,
                    ActivateGuid = Guid.NewGuid(),
                    ProfileImage = "common.png",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    isActivate = true,
                    isAdmin = false,
                    email = user.email

                });
                return user;


            }
            else
            {
                return null;
            }
        }
        public void UpdateUser(User user, string pic)
        {
            bool girdiMi = false;
            User CurrentUser = HttpContext.Current.Session["login"] as User;

            User findUserIfExistUSERNAME = command.Find(y => y.UserName == user.UserName);
            User findUserIfExistEMAIL = command.Find(y => y.email == user.email);
            if (user != null)
            {
                if (findUserIfExistUSERNAME != null)
                {
                    if (findUserIfExistUSERNAME.UserID != CurrentUser.UserID)
                    {
                        Errors.Add("Kayıtlı kullanıcı adı");
                        HttpContext.Current.Session["editError"] = Errors;
                        girdiMi = true;
                    }
                }
                else if (findUserIfExistEMAIL != null)
                {
                    if (findUserIfExistEMAIL.UserID != CurrentUser.UserID)
                    {
                        Errors.Add("Kayıtlı eposta");
                        HttpContext.Current.Session["editError"] = Errors;
                        girdiMi = true;
                    }
                }

            }
            if (girdiMi == false)
            {
                User updateUser = command.Find(xz => xz.UserID == user.UserID);

                updateUser.UserName = user.UserName;
                updateUser.Name = user.Name;
                updateUser.Password = user.Password;
                updateUser.email = user.email;
                updateUser.Surname = user.Surname;
                updateUser.ProfileImage = pic;

                int x = command.Save();
                HttpContext.Current.Session["login"] = updateUser;
                HttpContext.Current.Session["deger"] = x;
            }

        }
        public User FindUser(int? id)
        {
            return command.Find(x => x.UserID == id);
        }
        public User FindUser(string kullaniciAd)
        {
            return command.Find(x => x.UserName == kullaniciAd);
        }
        public User FindUserEM(string email)
        {
            return command.Find(x => x.email == email);
        }
        public void ForgotPassword(User user)
        {
            User forgotUser = command.Find(x => x.UserID == user.UserID);
            if (forgotUser != null)
            {
                string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                string activateUri = $"{siteUri}/Home/ForgetPW/{Guid.NewGuid()}";
                string body = $"Merhaba {forgotUser.UserName}! Hesabınızı aktifleştirmek için <a href='{activateUri}'>tıklayınız</a>.";
                MailHelper.SendMail(body, forgotUser.email, "Code Forum şifre yenileme");
                HttpContext.Current.Session["message"] = "E-postanızı kontrol ediniz.";
            }
            else
            {
                HttpContext.Current.Session["message"] = "Hata oluştu. Lütfen kullanıcı adınızın ve epostanızın eşleştiğinden emin olun.";
            }
        }
        public int ForgotPasswordOK(User user)
        {
            if (user != null)
            {
                User forgUser = command.Find(x => x.UserName == user.UserName);
                if (forgUser != null)
                {
                    forgUser.Password = user.Password;
                    int x = command.Save();
                    return x;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }

        }
        public int DeleteUser(User user)
        {
            User deleteUser = command.Find(xz => xz.UserID == user.UserID);
            
            for (int i = 0; i < user.post.Count; i++)
            {
                for (int k = 0; k < user.post[i].like.Count; k++)
                {
                    Like selectedLike = user.post[i].like[k];
                    Like thelike = commandLike.Find(y => y.Id == selectedLike.Id);
                    commandLike.Delete(thelike);
                }
                for (int k = 0; k < user.post[i].comment.Count; k++)
                {
                    Comments selectedcomment = user.post[i].comment[k];
                    Comments theComment = commandComments.Find(y => y.CommentID == selectedcomment.CommentID);
                    commandComments.Delete(theComment);
                }
                user.post[i].like.Clear();
                user.post[i].comment.Clear();
                Post selectedPost = user.post[i];
                Post thepost = commandPost.Find(y => y.PostID == selectedPost.PostID);
                commandPost.Delete(thepost);
            }
            for (int k = 0; k < user.like.Count; k++)
            {
                Like selectedLike = user.like[k];
                Like thelike = commandLike.Find(y => y.Id == selectedLike.Id);
                if (thelike != null)
                {
                    commandLike.Delete(thelike);
                }
            }
            for (int k = 0; k < user.comment.Count; k++)
            {
                Comments selectedcomment = user.comment[k];
                Comments theComment = commandComments.Find(y => y.CommentID == selectedcomment.CommentID);
                if (theComment != null)
                {
                    commandComments.Delete(theComment);
                }
            }
            deleteUser.post.Clear();
            deleteUser.like.Clear();
            deleteUser.comment.Clear();

            int complete = command.Delete(deleteUser);
            if (complete > 0)
            {
                return 1;
            }
            else
            {
                Errors.Add("Kullanıcı silinemedi");
                return 0;
            }
        }
        

    }

}

