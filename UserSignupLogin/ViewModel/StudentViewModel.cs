using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserSignupLogin.Models;

namespace UserSignupLogin.ViewModel
{
    public class StudentViewModel
    {
        public IEnumerable<TBLUserInfo> TBLUserInfoes { get; set; }
        public IEnumerable<StudentDetail> StudentDetails { get; set; }

        public IEnumerable<SubjectDetail> SubjectDetails { get; set; }
    }
}