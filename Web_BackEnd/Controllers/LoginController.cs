using ProjectPool.Library;
using ProjectPool.Models;
using ProjectPool.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectPool.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel user)
        {
            LoginService loginService = new LoginService(Common.GetConnectionString());
            if (loginService.CheckUser(user))
            {
                loginService.SetUserSession(user);
                return RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        public ActionResult ChangePassword()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(LoginViewModel user)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var loginService = new LoginService(Common.GetConnectionString());
            var user1 = loginService.Load(x => x.Username == user.username).FirstOrDefault();
            
            var oldPassword = Common.GenerateHash(user.password, user1.Salt);

            if (oldPassword == user1.Password)
            {
                var newPassword = Common.GenerateHash(user.newPassword, user1.Salt);
                user1.Password = newPassword;
                loginService.Save(user1, true);
            }
            
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return View("Login");
        }

    }
}
