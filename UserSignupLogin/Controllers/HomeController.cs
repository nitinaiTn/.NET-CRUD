using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserSignupLogin.Models;
namespace UserSignupLogin.Controllers
{
    public class HomeController : Controller
    {
        DBuserSignupLoginEntities db = new DBuserSignupLoginEntities();
        private object tBLUserInfo;

        // GET: Home
        public ActionResult Index()
        {
            return View(db.TBLUserInfoes.ToList());
        }

        public ActionResult Signup() 
        { 
            return View(); 
        }

        [HttpPost]
        public ActionResult Signup(TBLUserInfo tBLUserInfo)
        {
            if(db.TBLUserInfoes.Any( x=> x.UserUs == tBLUserInfo.UserUs))
            {
                ViewBag.Notification = "This Accout has already existed";
                return View();
            }
            else
            {
                db.TBLUserInfoes.Add(tBLUserInfo);
                db.SaveChanges();
                Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
                Session["UserUsSS"] = tBLUserInfo.UserUs.ToString();
                return RedirectToAction("Index", "Home");
            }
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(TBLUserInfo tBLUserInfo)
        {
            var checkLogin = db.TBLUserInfoes.Where(x => x.UserUs.Equals(tBLUserInfo.UserUS) && x.PasswordUs.Equals(tBLUserInfo.PasswordUS)).FirstOrDefault();
            if(checkLogin != null)
            {
                Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
                Session["UserUsSS"] = tBLUserInfo.UserUs.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Notification("Wrong username and password");
            }
            return View();
        }
    }
}