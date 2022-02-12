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
    public class DifficultiesController : Controller
    {
        private NEContext db = new NEContext();

        // GET: Difficulties
        public ActionResult Index()
        {
            return View(db.Difficulties.ToList());
        }

        // GET: Difficulties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Difficulty difficulty = db.Difficulties.Find(id);
            if (difficulty == null)
            {
                return HttpNotFound();
            }
            return View(difficulty);
        }

        // GET: Difficulties/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Difficulties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Level")] Difficulty difficulty)
        {
            if (ModelState.IsValid)
            {
                db.Difficulties.Add(difficulty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(difficulty);
        }

        // GET: Difficulties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Difficulty difficulty = db.Difficulties.Find(id);
            if (difficulty == null)
            {
                return HttpNotFound();
            }
            return View(difficulty);
        }

        // POST: Difficulties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Level")] Difficulty difficulty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(difficulty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(difficulty);
        }

        // GET: Difficulties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Difficulty difficulty = db.Difficulties.Find(id);
            if (difficulty == null)
            {
                return HttpNotFound();
            }
            return View(difficulty);
        }

        // POST: Difficulties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Difficulty difficulty = db.Difficulties.Find(id);
            db.Difficulties.Remove(difficulty);
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
