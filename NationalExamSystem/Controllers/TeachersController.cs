using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NationalExamSystem.Models;
using System.IO;

namespace NationalExamSystem.Controllers
{
    public class TeachersController : Controller
    {
        private NEContext db = new NEContext();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {
            Teacher tea = db.Teachers.Where(t => t.Email.Equals(Email) && t.Password.Equals(Password)).FirstOrDefault();
            if(tea==null)
            {
                Session["tec"] = "Invalid Email or Password";
                ViewBag.msg  = "Invalid Email or Password";
                return View();

            }
            else
            {
                Session["tec"] = tea.TeacherId;
                return RedirectToAction("index", "Admin");
            }


        }

        public ActionResult Logout()
        {
            Session["tec"] = null;
            Session.Abandon();
            Session.Clear();

            return RedirectToAction("Login");
        }

        public ActionResult TeacherTest(Question q = null)
        {
            var e = db.Choosen_Question.ToList();
            ViewBag.e = e;
            if (q != null)
            {
                List<Option> ops = db.Options.Where(s => s.QuestionId == q.Id).ToList();
                ViewBag.ops = ops;
                return View(q);
            }
            else
                return View();
        }

        public ActionResult next(int id)
        {
            // Question q = db.Questions.Where(s=>s.Id==id).FirstOrDefault();
            Question q = db.Choosen_Question.Where(s => s.questionId == id).Select(aq => aq.Question).FirstOrDefault();
            return RedirectToAction("Exam", q);
        }

        static List<QuestionModel> list = new List<QuestionModel>();
        static double mark = 0;
        int flag = 0;

        public ActionResult postans(int quesid, string option, int OptionId)
        {

            var correct = db.Choosen_Question.Where(s => s.questionId == quesid).Select(a => new
            {
                a.Question,
                a.Question.Mark,
                opId = a.Question.Options.Select(z => z.OptionId),
                ans = a.Question.Options.Where(q => q.CorrectAnswer == true).Select(x => x.Answer).FirstOrDefault()
            }).FirstOrDefault();

            foreach (var i in list)
            {
                if (i.qid == quesid)
                {
                    flag = 1;
                    i.option = OptionId;
                    i.opans = option;
                    if (i.opans == correct.ans)
                    {
                        i.mark = (double)correct.Mark;
                    }
                    else
                    {
                        i.mark = 0;
                    }
                }
            }
            if (flag == 0)
            {
                QuestionModel ql = new QuestionModel();
                ql.qid = quesid;
                ql.question = db.Choosen_Question.Where(s => s.questionId == quesid).Select(d => d.Question.Question1).FirstOrDefault();
                ql.option = OptionId;

                ql.opans = option;

                if (ql.opans == correct.ans)
                {
                    ql.mark = (double)correct.Mark;
                }
                else
                    ql.mark = 0;
                list.Add(ql);
            }

            mark = 0;
            foreach (var i in list)
            {
                mark += i.mark;
            }

            Session["mark"] = mark;

            //show the user's answers
            if (QuestionModel.dic.ContainsKey(quesid))
            {
                QuestionModel.dic[quesid] = option;
            }
            else
            {
                QuestionModel.dic.Add(quesid, option);
            }

            return RedirectToAction("Exam");
        }

        public ActionResult EndExam()
        {

            string Mark = "0";
            if (Session["mark"] != null)
            {
                Mark = Session["mark"].ToString();
            }
            
            return RedirectToAction("index", "home");
        }

        // GET: Teachers
        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }

        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teachers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,Name,Birthdate,Phone,Specialization,Img,Email,Password")] Teacher teacher, HttpPostedFileBase Img)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/assets/img/"), Path.GetFileName(Img.FileName));
                Img.SaveAs(path);
                teacher.Img = Img.FileName;
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,Name,Birthdate,Phone,Specialization,Img,Email,Password")] Teacher teacher, HttpPostedFileBase Img)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/assets/img/"), Path.GetFileName(Img.FileName));
                Img.SaveAs(path);
                teacher.Img = Img.FileName;
                db.Entry(teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers.Find(id);
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
