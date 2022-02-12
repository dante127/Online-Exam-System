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
    public class CoursesController : Controller
    {
        private NEContext db = new NEContext();

        // GET: Courses
        public ActionResult Index()
        {
            int tid = int.Parse(Session["tec"].ToString());
            var courses = db.Courses.Include(c => c.Category).Include(c => c.Teacher).Where(s=>s.TeacherId==tid);
            return View(courses.ToList());
        }

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.CateId = new SelectList(db.Categories.Where(s=>s.Name.Equals("Software")), "Id", "Name");
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseName,Reference,TeacherId,Img,ShortDesc,LongDesc,CateId")] Course course, HttpPostedFileBase Img, HttpPostedFileBase Reference)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/assets/img/"), Path.GetFileName(Img.FileName));
                string refpath = Path.Combine(Server.MapPath("~/assets/Slides/"), Path.GetFileName(Reference.FileName));

                Img.SaveAs(path);
                Reference.SaveAs(refpath);
                course.Img = Img.FileName;
                course.Reference = Reference.FileName;

                db.Courses.Add(course);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CateId = new SelectList(db.Categories, "Id", "Name", course.CateId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", course.TeacherId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.CateId = new SelectList(db.Categories, "Id", "Name", course.CateId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", course.TeacherId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseName,Reference,TeacherId,Img,ShortDesc,LongDesc,CateId")] Course course, HttpPostedFileBase Img, HttpPostedFileBase Reference)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/assets/img/"), Path.GetFileName(Img.FileName));
                string refpath = Path.Combine(Server.MapPath("~/assets/Slides/"), Path.GetFileName(Reference.FileName));

                Img.SaveAs(path);
                Reference.SaveAs(refpath);
                course.Img = Img.FileName;
                course.Reference = Reference.FileName;
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CateId = new SelectList(db.Categories, "Id", "Name", course.CateId);
            ViewBag.TeacherId = new SelectList(db.Teachers, "TeacherId", "Name", course.TeacherId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
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
