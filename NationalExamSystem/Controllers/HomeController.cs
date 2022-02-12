using NationalExamSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationalExamSystem.Controllers
{
    public class HomeController : Controller
    {
        private NEContext db = new NEContext();
        public ActionResult Index()
        {
            List<Category> categories = db.Categories.ToList();
            List<Course> PopularCourses = db.Courses.Take(3).ToList();
            ViewData["pop"] = PopularCourses;            
            return View(categories);
        }
       
        public ActionResult CateCrs(int cateid)
        {
            List<Course> crs = db.Courses.Where(c => c.CateId == cateid).ToList();
         
            return View("courses",crs);
        }
        public ActionResult Courses()
        {
            List<Course> crs = db.Courses.ToList();
            return View(crs);
        }

        
        public ActionResult CourseDetails(int id)
        {
            Course crs = db.Courses.Find(id);
            crs.Teacher = db.Teachers.Where(a => a.TeacherId == crs.TeacherId).FirstOrDefault();
            crs.Category = db.Categories.Where(a => a.Id == crs.CateId).FirstOrDefault();
            var pup = db.Courses.Where(c => c.CateId == crs.CateId && c.CourseId!=id).ToList();
            ViewBag.pup = pup;
            return View(crs);
        }

        public FileResult Download(string file)
        {
            string path = Path.Combine(Server.MapPath("~/assets/Slides/"), Path.GetFileName(file));

            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            string fileName = file;
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(string Email ,string NationNumber)
        {
            if(Session["login"] != null)
            {
                return RedirectToAction("index", "home");
            }

            var stu = db.Students.Where(s => s.Email.Equals(Email) && s.NationNumber.Equals(NationNumber)).FirstOrDefault();
            if (stu == null)
            {
                Session["stumsg"] = "incorrect email or password";
                return RedirectToAction("Login");
            }
            else
            {
                Session["login"] = stu.Email;
                return RedirectToAction("index", "home");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {

            ViewBag.ExamId = new SelectList(db.Exams, "ExamId", "ExamName");

            return View();
        }

        [HttpPost]
        public ActionResult Register( Student stu)
        {

            var s = db.Students.Where(a => a.Email.Equals(stu.Email)).FirstOrDefault();
            if (s == null)
            {                
                db.Students.Add(stu);
                db.SaveChanges();
                Session["reg"] = "The Register is Done";
            }
            else
            {
                Session["reg"] = "Already Registerd Account";

            }
            return RedirectToAction("Register");
        }

        public ActionResult Logout()
        {
            Session["login"] = null;
            Session.Abandon();
            Session.Clear();
          
            return RedirectToAction("Login");
        }

        public ActionResult UserProfile(string email)
        {

            if (Session["login"] == null)
            {
                return RedirectToAction("Login", "home");
            }

            Student stu = db.Students.Where(s => s.Email.Equals(email)).FirstOrDefault();
            Exam e = db.Exams.Where(s => s.ExamId == stu.ExamId).FirstOrDefault();

            stu.Exam = e;
            var pending = db.StudentExams.Where(ma => ma.StudentId == stu.StudentId && ma.ExamId == stu.ExamId).FirstOrDefault();
            if (pending != null) { 
                 double Mark = (double) db.StudentExams.Where(ma => ma.StudentId == stu.StudentId && ma.ExamId == stu.ExamId).FirstOrDefault().Mark;
                    Session["stumark"] = Mark;
                stu.Exam.IsActive = false;
                }
            if (stu == null)
            {
                return RedirectToAction("Login");
            }
            else
            {

                return View(stu);
            }
        }

        public ActionResult Contact()
        {
           

            return View();
        }

        //NEContext db = new NEContext();
        //public ActionResult GenerateRandom()
        //{
        //    var ran = (from p in db.Choosen_Question orderby Guid.NewGuid() select p).Take(10).ToList();
        //    return View();
        //}

    }
}