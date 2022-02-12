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
    public class OptionsController : Controller
    {
        private NEContext db = new NEContext();

        // GET: Options
        public ActionResult Index()
        {
            int tid = int.Parse(Session["tec"].ToString());
            var options = db.Options.Include(o => o.Question).Where(s=>s.Question.TeacherId==tid);
            return View(options.ToList());
        }

        // GET: Options/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option option = db.Options.Find(id);
            if (option == null)
            {
                return HttpNotFound();
            }
            return View(option);
        }

        // GET: Options/Create
        public ActionResult Create()
        {
            int tid = int.Parse(Session["tec"].ToString());
            ViewBag.QuestionId = new SelectList(db.Questions.Where(s=>s.TeacherId==tid), "Id", "Question1");
            return View();
        }

        // POST: Options/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OptionId,Answer,CorrectAnswer,QuestionId")] Option option)
        {
            if (ModelState.IsValid)
            {
                db.Options.Add(option);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Question1", option.QuestionId);
            return View(option);
        }

        // GET: Options/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option option = db.Options.Find(id);
            if (option == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Question1", option.QuestionId);
            return View(option);
        }

        // POST: Options/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OptionId,Answer,CorrectAnswer,QuestionId")] Option option)
        {
            if (ModelState.IsValid)
            {
                db.Entry(option).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionId = new SelectList(db.Questions, "Id", "Question1", option.QuestionId);
            return View(option);
        }

        // GET: Options/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Option option = db.Options.Find(id);
            if (option == null)
            {
                return HttpNotFound();
            }
            return View(option);
        }

        // POST: Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Option option = db.Options.Find(id);
            db.Options.Remove(option);
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
