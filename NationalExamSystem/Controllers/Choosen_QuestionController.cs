using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NationalExamSystem.Models;

namespace NationalExamSystem.Controllers
{
    public class Choosen_QuestionController : Controller
    {
        private NEContext db = new NEContext();

        // GET: Choosen_Question
        public ActionResult Index()
        {
            var choosen_Question = db.Choosen_Question.Include(c => c.Exam).Include(c => c.Question);
            return View(choosen_Question.ToList());
        }

        // GET: Choosen_Question/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choosen_Question choosen_Question = db.Choosen_Question.Find(id);
            if (choosen_Question == null)
            {
                return HttpNotFound();
            }
            return View(choosen_Question);
        }

        // GET: Choosen_Question/Create
        public ActionResult Create()
        {
            ViewBag.ExamId = new SelectList(db.Exams, "ExamId", "ExamName");
            ViewBag.questionId = new SelectList(db.Questions, "Id", "Question1");
            return View();
        }

        // POST: Choosen_Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ExamId,questionId")] Choosen_Question choosen_Question)
        {
            if (ModelState.IsValid)
            {
                db.Choosen_Question.Add(choosen_Question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamId = new SelectList(db.Exams, "ExamId", "ExamName", choosen_Question.ExamId);
            ViewBag.questionId = new SelectList(db.Questions, "Id", "Question1", choosen_Question.questionId);
            return View(choosen_Question);
        }

        // GET: Choosen_Question/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choosen_Question choosen_Question = db.Choosen_Question.Find(id);
            if (choosen_Question == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamId = new SelectList(db.Exams, "ExamId", "ExamName", choosen_Question.ExamId);
            ViewBag.questionId = new SelectList(db.Questions, "Id", "Question1", choosen_Question.questionId);
            return View(choosen_Question);
        }

        // POST: Choosen_Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ExamId,questionId")] Choosen_Question choosen_Question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choosen_Question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamId = new SelectList(db.Exams, "ExamId", "ExamName", choosen_Question.ExamId);
            ViewBag.questionId = new SelectList(db.Questions, "Id", "Question1", choosen_Question.questionId);
            return View(choosen_Question);
        }

        // GET: Choosen_Question/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choosen_Question choosen_Question = db.Choosen_Question.Find(id);
            if (choosen_Question == null)
            {
                return HttpNotFound();
            }
            return View(choosen_Question);
        }

        // POST: Choosen_Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choosen_Question choosen_Question = db.Choosen_Question.Find(id);
            db.Choosen_Question.Remove(choosen_Question);
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
