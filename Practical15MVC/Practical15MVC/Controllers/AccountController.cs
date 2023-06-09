using Practical15MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Practical15MVC.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserModel model)
        {

            using (MVC_SecurityEntities db = new MVC_SecurityEntities())
            {


                bool IsValidUser = db.Users.Any(user => user.UserName.ToLower() == model.UserName.ToLower() &&
                user.UserPassword.ToLower() == model.UserPassword.ToLower());
                if (IsValidUser)
                {

                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees");


                }
                ModelState.AddModelError("", "Invalid Username or Password");
                return View();

            }


        }

        public ActionResult Logout()
        {


            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");


        }

        public ActionResult SignUp()
        {


            return View();

        }
        [HttpPost]
        public ActionResult SignUp(User model)
        {


            using (MVC_SecurityEntities db =new MVC_SecurityEntities())
            {

                db.Users.Add(model);
                db.SaveChanges();


            }
            return RedirectToAction("Login", "Account");
        }
    }
}