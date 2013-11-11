using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ExpenseTrackingSystem.Web.Models;

namespace ExpenseTrackingSystem.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        [AllowAnonymous]
        public ActionResult Registration()
        {
            return View(new UserModels.RegisterUserModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(UserModels.RegisterUserModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    MembershipCreateStatus createStatus;
                    Membership.CreateUser(model.Login, model.Password, null, null, null, true, null, out createStatus);

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        ViewBag.Mess = "Welcome to the system ! Your registration was successful :)";

                        return View(model);
                    }

                    ModelState.AddModelError("name", createStatus.ToString());
                }
                catch (MembershipCreateUserException e)
                {
                    MvcApplication.log.ErrorException(" Exeption during save data in the database: " + e.StatusCode, e);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new UserModels.LoginUserModel());
        }

        // POST: /User/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModels.LoginUserModel model, string returnUrl)
        {
            if (ModelState.IsValid && Membership.ValidateUser(model.UserName, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);

                return RedirectToAction("Index", "Home");
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");

            return View(model);
        }

        //
        // POST: /User/LogOff

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

    }
}
