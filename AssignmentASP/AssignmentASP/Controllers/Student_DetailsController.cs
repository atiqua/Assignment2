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
    public class Student_DetailsController : Controller
    {
        private ModelDBEntities db = new ModelDBEntities();

        // GET: Student_Details
        public ActionResult Index()
        {
            var student_Details = db.Student_Details.Include(s => s.Staff_Details);
            return View(student_Details.ToList());
        }

        // GET: Student_Details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Details student_Details = db.Student_Details.Find(id);
            if (student_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Details);
        }

        // GET: Student_Details/Create
        public ActionResult Create()
        {
            ViewBag.StaffId = new SelectList(db.Staff_Details, "StaffId", "StaffName");
            return View();
        }

        // POST: Student_Details/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,StaffId")] Student_Details student_Details)
        {
            if (ModelState.IsValid)
            {
                db.Student_Details.Add(student_Details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffId = new SelectList(db.Staff_Details, "StaffId", "StaffName", student_Details.StaffId);
            return View(student_Details);
        }

        // GET: Student_Details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Details student_Details = db.Student_Details.Find(id);
            if (student_Details == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffId = new SelectList(db.Staff_Details, "StaffId", "StaffName", student_Details.StaffId);
            return View(student_Details);
        }

        // POST: Student_Details/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,StaffId")] Student_Details student_Details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffId = new SelectList(db.Staff_Details, "StaffId", "StaffName", student_Details.StaffId);
            return View(student_Details);
        }

        // GET: Student_Details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Details student_Details = db.Student_Details.Find(id);
            if (student_Details == null)
            {
                return HttpNotFound();
            }
            return View(student_Details);
        }

        // POST: Student_Details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student_Details student_Details = db.Student_Details.Find(id);
            db.Student_Details.Remove(student_Details);
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
