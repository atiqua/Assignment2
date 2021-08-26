using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AssignmentASP.Models;

namespace AssignmentASP.Controllers
{
    public class Staff_DetailsController : Controller
    {
        private ModelDBEntities db = new ModelDBEntities();

        // GET: Staff_Details
        public ActionResult Index()
        {
            var staff_Details = db.Staff_Details.Include(s => s.Cours);
            return View(staff_Details.ToList());
        }

        // GET: Staff_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff_Details staff_Details = db.Staff_Details.Find(id);
            if (staff_Details == null)
            {
                return HttpNotFound();
            }
            return View(staff_Details);
        }

        // GET: Staff_Details/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName");
            return View();
        }

        // POST: Staff_Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StaffId,StaffName,CourseId")] Staff_Details staff_Details)
        {
            if (ModelState.IsValid)
            {
                db.Staff_Details.Add(staff_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", staff_Details.CourseId);
            return View(staff_Details);
        }

        // GET: Staff_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff_Details staff_Details = db.Staff_Details.Find(id);
            if (staff_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", staff_Details.CourseId);
            return View(staff_Details);
        }

        // POST: Staff_Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StaffId,StaffName,CourseId")] Staff_Details staff_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(staff_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseName", staff_Details.CourseId);
            return View(staff_Details);
        }

        // GET: Staff_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff_Details staff_Details = db.Staff_Details.Find(id);
            if (staff_Details == null)
            {
                return HttpNotFound();
            }
            return View(staff_Details);
        }

        // POST: Staff_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Staff_Details staff_Details = db.Staff_Details.Find(id);
            db.Staff_Details.Remove(staff_Details);
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
