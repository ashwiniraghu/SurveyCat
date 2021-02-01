using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SurveyCat.Models;

namespace SurveyCat.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(User userModel)
        {
            User currentUser = DatabaseHelper.VerifyUserDetails(userModel);
            string result;
            if (currentUser != null)
            {
                Session["UserID"] = currentUser.UserID;
                Session["UserName"] = currentUser.Login;
                result = "Admin";
                return Json(result, JsonRequestBehavior.AllowGet);
                //return RedirectToAction("Index", "QuestionBank");
            }
            else
            {
                //userModel.ErrorMessage = "Wrong User Name or password";
                result = "fail";
                //return View("Login", userModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public JsonResult Register(User userModel)
        {
            User currentUser = DatabaseHelper.VerifyUserDetails(userModel);
            string result;
            //if (currentUser == null)
            {
              
                result = "fail";
                //return View("Login", userModel);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
    }
}