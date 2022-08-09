using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using UserSignupLogin.Models;
using UserSignupLogin.ViewModel;
namespace UserSignupLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBuserSignupLoginEntities2 db = new DBuserSignupLoginEntities2();

        // GET: Home

        //public ActionResult Index()
        //{
        //    var tables = new StudentViewModel()
        //    {
        //        StudentDetails = db.StudentDetails.ToList(),
        //        SubjectDetails = db.SubjectDetails.ToList(),
        //        TBLUserInfoes = db.TBLUserInfoes.ToList()
        //    };
        //    return View(tables);
        //}
        String[] search = new string[3];
        public ActionResult Index(String searchUser, String searchStudent, String searchSubject)
        {
            if ((String.IsNullOrEmpty(searchUser)) && (String.IsNullOrEmpty(searchStudent)) && (String.IsNullOrEmpty(searchSubject)))//Not Search
                {
                var tables = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.ToList(),
                    SubjectDetails = db.SubjectDetails.ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.ToList()
                };
                return View(tables);
            }
            else if (!(String.IsNullOrEmpty(searchUser)) && !(String.IsNullOrEmpty(searchStudent)) && !(String.IsNullOrEmpty(searchSubject)))//search UserName && StudentName && SubjectName
            {
                var tables = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.Where(x => x.StudentName.StartsWith(searchStudent)).ToList(),
                    SubjectDetails = db.SubjectDetails.Where(x => x.SubjectName.StartsWith(searchSubject)).ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.Where(x => x.UserUs.StartsWith(searchUser)).ToList()
                };
                return View(tables);
            }
            else if (!(String.IsNullOrEmpty(searchUser))&&!(String.IsNullOrEmpty(searchStudent)))//search UserName && Student Name
            {
                var tables = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.Where(x => x.StudentName.StartsWith(searchStudent)).ToList(),
                    SubjectDetails = db.SubjectDetails.ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.Where(x => x.UserUs.StartsWith(searchUser)).ToList()
                };
                return View(tables);
            }
            else if (!(String.IsNullOrEmpty(searchUser))&&!(String.IsNullOrEmpty(searchSubject)))//search UserName && SubjectName
            {
                var tables = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.ToList(),
                    SubjectDetails = db.SubjectDetails.Where(x => x.SubjectName.StartsWith(searchSubject)).ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.Where(x => x.UserUs.StartsWith(searchUser)).ToList()
                };
                return View(tables);
            }
            else if (!(String.IsNullOrEmpty(searchStudent)) && !(String.IsNullOrEmpty(searchSubject)))//search StudentName && SubjectName
            {
                var tables = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.Where(x => x.StudentName.StartsWith(searchStudent)).ToList(),
                    SubjectDetails = db.SubjectDetails.Where(x => x.SubjectName.StartsWith(searchSubject)).ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.ToList()
                };
                return View(tables);
            }

            else if (!(String.IsNullOrEmpty(searchUser)))//search UserName
            {
                var tables = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.ToList(),
                    SubjectDetails = db.SubjectDetails.ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.Where(x => x.UserUs.StartsWith(searchUser)).ToList()
                };
                return View(tables);
            }
            else if (!(String.IsNullOrEmpty(searchStudent)))//search Student
            {
                var tables = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.Where(x => x.StudentName.StartsWith(searchStudent)).ToList(),
                    SubjectDetails = db.SubjectDetails.ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.ToList()
                };
                return View(tables);
            }
            else if (!(String.IsNullOrEmpty(searchSubject)))//Search Subject
            {
                var tables = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.ToList(),
                    SubjectDetails = db.SubjectDetails.Where(x => x.SubjectName.StartsWith(searchSubject)).ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.ToList()
                };
                return View(tables);
            }
            
            else
            {
                return View();
            }
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
                MessageBox.Show("Create Success", " Create Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                StreamWriter file = new StreamWriter("d:\\LogTest.txt",append: true);
                file.WriteLine("Log Create At Time : "+DateTime.Now.ToString());
                file.WriteLine("Create Name = " + tBLUserInfo.UserUs.ToString());
                file.WriteLine("Create Pasword = "+tBLUserInfo.PasswordUs.ToString());
                file.WriteLine("");
                file.Close();
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

        public ActionResult Login(TBLUserInfo objUser)
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
            //using (var context = new DBuserSignupLoginEntities())
            //{
               
                    
            //}
        }

        //edit module
        public ActionResult Edit(int idUs)
        {
            using (var context = new DBuserSignupLoginEntities2())
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
            using (var context = new DBuserSignupLoginEntities2())
            {
                var data = context.TBLUserInfoes.FirstOrDefault(x => x.IdUs == idUs);
                if (data != null)
                {
                    data.UserUs = model.UserUs;
                    data.PasswordUs = model.PasswordUs;
                    StreamWriter file = new StreamWriter("d:\\LogTest.txt", append: true);
                    file.WriteLine("Log Edit At Time : " + DateTime.Now.ToString());

                    file.WriteLine("Edit UserName : " + model.UserUs.ToString());
                    context.SaveChanges();
                    file.WriteLine("UserName After Edit : " + model.UserUs.ToString() + " && " + "Password After Edit : " + model.PasswordUs.ToString());
                    file.WriteLine("");
                    file.Close();
                    MessageBox.Show("Edit Success", " Edit Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            using (var context = new DBuserSignupLoginEntities2())
            {
                var data = context.TBLUserInfoes.FirstOrDefault(x => x.IdUs == idUs);
                if (data != null)
                {
                    StreamWriter file = new StreamWriter("d:\\LogTest.txt", append: true);
                    file.WriteLine("Log Delete At Time : " + DateTime.Now.ToString());
                    file.WriteLine("");
                    file.Close();
                    context.TBLUserInfoes.Remove(data);
                    context.SaveChanges();
                    MessageBox.Show("Delete Success", " Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return RedirectToAction("Index");
                }
                else
                    return View();
            }
        }

        //Calculator moudle
        public ActionResult Calculate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Calculate(string firstNumber, string secondNumber, string Cal)
        {
            CheckNullErr(firstNumber, secondNumber);
            int a = Convert.ToInt32(firstNumber);
            int b = Convert.ToInt32(secondNumber);
            int c = 0;
            
            switch (Cal)
            {
                case "Add":
                    c = a + b;
                    break;
                case "Sub":
                    c = a - b;
                    break;
                case "Mul":
                    c = a * b;
                    break;
                case "Div":
                    CheckDivZero(a , b, c);
                    break;
            }
            ViewBag.Result = c;
            return View();
        }

        public ActionResult CheckDivZero(int a, int b, int c)
        {
            if(b != 0)
            {
                c = a / b;
            }
            else
            {
                ViewBag.DivZero = "Div By Zero Please Try Agian";
            }
            return View();
        }

        public ActionResult CheckNullErr(string FNum, string SNum)
        {
            if (FNum != null && SNum != null)
            {
                return RedirectToAction("Calculate");
            }
            else
            {
                ViewBag.NullErr = "Please Insert Number In Form";
                return View();
            }
        }
    }
}