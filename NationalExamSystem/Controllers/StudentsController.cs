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
    public class StudentsController : Controller
    {
        private NEContext db = new NEContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Exam);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.ExamId = new SelectList(db.Exams, "ExamId", "ExamName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,Name,birthdate,phone,NationNumber,InvoiceImage,InvoiceNumber,ExamId,Email")] Student student, HttpPostedFileBase InvoiceImage)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/assets/img/"), Path.GetFileName(InvoiceImage.FileName));
                InvoiceImage.SaveAs(path);
                student.InvoiceImage = InvoiceImage.FileName;
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ExamId = new SelectList(db.Exams, "ExamId", "ExamName", student.ExamId);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.ExamId = new SelectList(db.Exams, "ExamId", "ExamName", student.ExamId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,Name,birthdate,phone,NationNumber,InvoiceImage,InvoiceNumber,ExamId,Email")] Student student, HttpPostedFileBase InvoiceImage)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/assets/img/"), Path.GetFileName(InvoiceImage.FileName));
                InvoiceImage.SaveAs(path);
                student.InvoiceImage = InvoiceImage.FileName;
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ExamId = new SelectList(db.Exams, "ExamId", "ExamName", student.ExamId);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
