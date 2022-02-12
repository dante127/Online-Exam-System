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
    public class QuestionsController : Controller
    {
        private NEContext db = new NEContext();

        // GET: Questions
        public ActionResult Index()
        {
            int tid = int.Parse(Session["tec"].ToString());
            var questions = db.Questions.Include(q => q.Category).Include(q => q.Difficulty).Include(q => q.Teacher).Where(s=>s.TeacherId==tid);
            return View(questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.Where(s=>s.Name.Equals("Software")), "Id", "Name");
            ViewBag.DifficultyId = new SelectList(db.Difficulties, "Id", "Level");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Question1,QuestionFile,Img,Mark,CategoryId,DifficultyId,TeacherId")] Question question, HttpPostedFileBase Img=null)
        {
            string path = "-1";
            if (ModelState.IsValid)
            {
                path = Path.Combine(Server.MapPath("~/assets/img/questions/"), Path.GetFileName(Img.FileName));
                Img.SaveAs(path);
                question.Img = Img.FileName;


                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", question.CategoryId);
            ViewBag.DifficultyId = new SelectList(db.Difficulties, "Id", "Level", question.DifficultyId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", question.TeacherId);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", question.CategoryId);
            ViewBag.DifficultyId = new SelectList(db.Difficulties, "Id", "Level", question.DifficultyId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", question.TeacherId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Question1,QuestionFile,Img,Mark,CategoryId,DifficultyId,TeacherId")] Question question, HttpPostedFileBase Img = null)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/assets/img/questions/"), Path.GetFileName(Img.FileName));
                Img.SaveAs(path);
                question.Img = Img.FileName;

                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", question.CategoryId);
            ViewBag.DifficultyId = new SelectList(db.Difficulties, "Id", "Level", question.DifficultyId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", question.TeacherId);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            foreach(var i in db.Options.Where(a=>a.QuestionId==question.Id))
            {
                db.Options.Remove(i);
            }

            db.Questions.Remove(question);
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
