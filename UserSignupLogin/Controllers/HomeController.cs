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

        //Login Module
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Login(TBLUserInfo tBLUserInfo)
        //{
        //    var checkLogin = db.TBLUserInfoes.Where(x => x.UserUs.Equals(tBLUserInfo.UserUs) && x.PasswordUs.Equals(tBLUserInfo.PasswordUs)).FirstOrDefault();
        //    if (checkLogin != null)
        //    {
        //        Session["IdUsSS"] = tBLUserInfo.IdUs.ToString();
        //        Session["UserUsSS"] = tBLUserInfo.UserUs.ToString();
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.Notification("Wrong username and password");
        //    }
        //    return View();
        //
        public ActionResult Login(TBLUserInfo objUser)
        {
            using (var context = new DBuserSignupLoginEntities())
            {
                var data = db.TBLUserInfoes.Where(a => a.UserUs.Equals(objUser.UserUs) && a.PasswordUs.Equals(objUser.PasswordUs)).FirstOrDefault();
                if (data != null)
                {
                    Session["UserID"] = data.IdUs.ToString();
                    Session["UserName"] = data.UserUs.ToString();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Message = "Wrong Try Again";
                    return View();
                }
                    
            }
        }

        //edit module
        public ActionResult Edit(int idUs)
        {
            using (var context = new DBuserSignupLoginEntities())
            {
                var data = context.TBLUserInfoes.Where(x => x.IdUs == idUs).SingleOrDefault();
                return View(data);
            }
        }

        //Edit Module
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int idUs, TBLUserInfo model)
        {
            using (var context = new DBuserSignupLoginEntities())
            {

                // Use of lambda expression to access
                // particular record from a database
                var data = context.TBLUserInfoes.FirstOrDefault(x => x.IdUs == idUs);

                // Checking if any such record exist 
                if (data != null)
                {
                    data.UserUs = model.UserUs;
                    data.PasswordUs = model.PasswordUs;
                    context.SaveChanges();

                    // It will redirect to 
                    // the Read method
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
        }

        //Delete Module
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult
        Delete(int idUs)
        {
            using (var context = new DBuserSignupLoginEntities())
            {
                var data = context.TBLUserInfoes.FirstOrDefault(x => x.IdUs == idUs);
                if (data != null)
                {
                    context.TBLUserInfoes.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
        }
    }
}