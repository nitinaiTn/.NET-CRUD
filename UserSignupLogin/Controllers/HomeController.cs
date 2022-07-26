﻿using System;
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
        //
        public ActionResult SortingCol(String sortUser, String sortPassword, String searchUser, String searchStudent, String searchSubject)
        {
            //ViewBag.SortingName = String.IsNullOrEmpty(SortRRR) ? "Sorting" : "NotSort";
            if (sortUser == "UserSort")
            {
                var sortt = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.ToList(),
                    SubjectDetails = db.SubjectDetails.ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.OrderByDescending(x => x.UserUs).ToList()
                };
                return View(sortt);
            }

            else if (sortPassword == "PasswordSort")
            {
                var sortt = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.ToList(),
                    SubjectDetails = db.SubjectDetails.ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.OrderByDescending(x => x.PasswordUs).ToList()
                };
                return View(sortt);
            }
            else if (sortUser == "UserSort" && !String.IsNullOrEmpty(searchUser))
            {
                var sortt = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.ToList(),
                    SubjectDetails = db.SubjectDetails.ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.Where(x => x.UserUs.StartsWith(searchUser) || x.PasswordUs.StartsWith(searchUser)).OrderByDescending(x => x.UserUs).ToList()
                };
                return View(sortt);
            }
            else if (sortUser == "UserNotSort")
            {
                var sortt = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.ToList(),
                    SubjectDetails = db.SubjectDetails.ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.OrderBy(x => x.UserUs).ToList()
                };
                return View(sortt);
            }
            else if (sortPassword == null)
            {
                var sortt = new StudentViewModel()
                {
                    StudentDetails = db.StudentDetails.ToList(),
                    SubjectDetails = db.SubjectDetails.ToList(),
                    TBLUserInfoes = db.TBLUserInfoes.OrderBy(x => x.PasswordUs).ToList()
                };
                return View(sortt);
            }
            else
            {
                return View();
            }
            
        }
        
        public ActionResult searchData(String searchUser, String searchStudent, String searchSubject, int Choice)
        {

            switch (Choice)
            {
                case 1: // Not Search
                    var tables1 = new StudentViewModel()
                    {
                        StudentDetails = db.StudentDetails.ToList(),
                        SubjectDetails = db.SubjectDetails.ToList(),
                        TBLUserInfoes = db.TBLUserInfoes.ToList()
                    };
                    return View(tables1);
                    break;
                case 2:// searchUser && Student && subject
                    var tables2 = new StudentViewModel()
                    {
                        StudentDetails = db.StudentDetails.Where(x => x.StudentName.StartsWith(searchStudent) || x.Major.StartsWith(searchStudent) || x.StudentID.ToString().StartsWith(searchStudent) || x.Year.ToString().StartsWith(searchStudent)).ToList(),
                        SubjectDetails = db.SubjectDetails.Where(x => x.SubjectName.StartsWith(searchSubject) || x.SubjectID.ToString().StartsWith(searchSubject) || x.TeacherName.StartsWith(searchSubject)).ToList(),
                        TBLUserInfoes = db.TBLUserInfoes.Where(x => x.UserUs.StartsWith(searchUser) || x.PasswordUs.StartsWith(searchUser)).ToList()
                    };
                    return View(tables2);
                    break;
                case 3:// searchUser && Student
                    var tables3 = new StudentViewModel()
                    {
                        StudentDetails = db.StudentDetails.Where(x => x.StudentName.StartsWith(searchStudent) || x.Major.StartsWith(searchStudent) || x.StudentID.ToString().StartsWith(searchStudent) || x.Year.ToString().StartsWith(searchStudent)).ToList(),
                        SubjectDetails = db.SubjectDetails.ToList(),
                        TBLUserInfoes = db.TBLUserInfoes.Where(x => x.UserUs.StartsWith(searchUser) || x.PasswordUs.StartsWith(searchUser)).ToList()
                    };
                    return View(tables3);
                    break;
                case 4: // searchUser &&  subject
                    var tables4 = new StudentViewModel()
                    {
                        StudentDetails = db.StudentDetails.ToList(),
                        SubjectDetails = db.SubjectDetails.Where(x => x.SubjectName.StartsWith(searchSubject) || x.SubjectID.ToString().StartsWith(searchSubject) || x.TeacherName.StartsWith(searchSubject)).ToList(),
                        TBLUserInfoes = db.TBLUserInfoes.Where(x => x.UserUs.StartsWith(searchUser) || x.PasswordUs.StartsWith(searchUser)).ToList()
                    };
                    return View(tables4);
                    break;
                case 5: // search Student && subject
                    var tables5 = new StudentViewModel()
                    {
                        StudentDetails = db.StudentDetails.Where(x => x.StudentName.StartsWith(searchStudent) || x.Major.StartsWith(searchStudent) || x.StudentID.ToString().StartsWith(searchStudent) || x.Year.ToString().StartsWith(searchStudent)).ToList(),
                        SubjectDetails = db.SubjectDetails.Where(x => x.SubjectName.StartsWith(searchSubject) || x.SubjectID.ToString().StartsWith(searchSubject) || x.TeacherName.StartsWith(searchSubject)).ToList(),
                        TBLUserInfoes = db.TBLUserInfoes.ToList()
                    };
                    return View(tables5);
                    break;
                case 6:// searchUser
                    var tables6 = new StudentViewModel()
                    {
                        StudentDetails = db.StudentDetails.ToList(),
                        SubjectDetails = db.SubjectDetails.ToList(),
                        TBLUserInfoes = db.TBLUserInfoes.Where(x => x.UserUs.StartsWith(searchUser) || x.PasswordUs.StartsWith(searchUser)).ToList()
                    };
                    return View(tables6);
                    break;
                case 7: // searchStudent
                    var tables7 = new StudentViewModel()
                    {
                        StudentDetails = db.StudentDetails.Where(x => x.StudentName.StartsWith(searchStudent) || x.Major.StartsWith(searchStudent) || x.StudentID.ToString().StartsWith(searchStudent) || x.Year.ToString().StartsWith(searchStudent)).ToList(),
                        SubjectDetails = db.SubjectDetails.ToList(),
                        TBLUserInfoes = db.TBLUserInfoes.ToList()
                    };
                    return View(tables7);
                    break;
                case 8: // searchsubject
                    var tables8 = new StudentViewModel()
                    {
                        StudentDetails = db.StudentDetails.ToList(),
                        SubjectDetails = db.SubjectDetails.Where(x => x.SubjectName.StartsWith(searchSubject) || x.SubjectID.ToString().StartsWith(searchSubject) || x.TeacherName.StartsWith(searchSubject)).ToList(),
                        TBLUserInfoes = db.TBLUserInfoes.ToList()
                    };
                    return View(tables8);
                    break;   
                default:
                    return View();
                    break;
            }
        }

        public ActionResult Index(String searchUser, String searchStudent, String searchSubject , String sortUser, String sortPassword)
        {
            if ((String.IsNullOrEmpty(searchUser)) && (String.IsNullOrEmpty(searchStudent)) && (String.IsNullOrEmpty(searchSubject)))//Not Search
            {
                if (sortUser != null || sortPassword != null)
                {
                    return SortingCol(sortUser, sortPassword, searchUser, searchStudent, searchSubject);
                }
                else
                {
                    return searchData(searchUser, searchStudent, searchSubject, 1);
                }
                
            }
            else if (!(String.IsNullOrEmpty(searchUser)) && !(String.IsNullOrEmpty(searchStudent)) && !(String.IsNullOrEmpty(searchSubject)))//search UserName && StudentName && SubjectName
            {
                return searchData(searchUser, searchStudent, searchSubject, 2);
            }
            else if (!(String.IsNullOrEmpty(searchUser))&&!(String.IsNullOrEmpty(searchStudent)))//search UserName && Student Name
            {
                return searchData(searchUser, searchStudent, searchSubject, 3);
            }
            else if (!(String.IsNullOrEmpty(searchUser))&&!(String.IsNullOrEmpty(searchSubject)))//search UserName && SubjectName
            {
                return searchData(searchUser, searchStudent, searchSubject, 4);
            }
            else if (!(String.IsNullOrEmpty(searchStudent)) && !(String.IsNullOrEmpty(searchSubject)))//search StudentName && SubjectName
            {
                return searchData(searchUser, searchStudent, searchSubject, 5);
            }

            else if (!(String.IsNullOrEmpty(searchUser)))//search UserName
            {
                return searchData(searchUser, searchStudent, searchSubject, 6);
            }
            else if (!(String.IsNullOrEmpty(searchStudent)))//search Student
            {
                return searchData(searchUser, searchStudent, searchSubject, 7);
            }
            else if (!(String.IsNullOrEmpty(searchSubject)))//Search Subject
            {
                return searchData(searchUser, searchStudent, searchSubject, 8);
            }
            else
            {
                return View();
            }

            ViewBag.txtUser = "ZXas";
            ViewBag.txtStudent = searchStudent;
            ViewBag.txtSubject = searchSubject;
            //ViewData["JavaScriptFunction"] = "GetAndSetValue();";
            //ViewBag.JavaScriptFunction = string.Format("GetAndSetValue('{0}','{1}','{2});", searchUser, searchStudent, searchSubject);
            //ViewBag.JavaScriptFunction = string.Format("GetAndSetValue();");
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
            //CheckNullErr(firstNumber, secondNumber);
            try
            {
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
                        c = a / b;
                        break;
                }
                ViewBag.Result = c;
            }
            catch (DivideByZeroException e)
            {
                ViewBag.DivZero = "Div By Zero Please Try Agian";
            }
            catch(FormatException e)
            {
                ViewBag.formatError = "Plese Insert Integer 0-9 Only In TextInputField";
            }
            catch(Exception e)
            {
                ViewBag.excepError = "Error Read Exception Fix It => " + e;
            }
       return View();
        }

        //public ActionResult CheckDivZero(int a, int b, int c)
        //{
        //    if(b != 0)
        //    {
        //        c = a / b;
        //    }
        //    else
        //    {
        //        ViewBag.DivZero = "Div By Zero Please Try Agian";
        //    }
        //    return View();
        //}

        //public ActionResult CheckNullErr(string FNum, string SNum)
        //{
        //    if (FNum != null && SNum != null)
        //    {
        //        return RedirectToAction("Calculate");
        //    }
        //    else
        //    {
        //        ViewBag.NullErr = "Please Insert Number In Form";
        //        return View();
        //    }
        //}
    }
}