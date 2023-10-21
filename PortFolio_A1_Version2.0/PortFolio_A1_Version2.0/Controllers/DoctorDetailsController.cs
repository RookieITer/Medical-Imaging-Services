using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PortFolio_A1_Version2._0.Models;

namespace PortFolio_A1_Version2._0.Controllers
{
    [Authorize]
    public class DoctorDetailsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DoctorDetails
        [Authorize(Roles = "Doctor")]
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            var doctors = from d in db.DoctorDetails
                          select d;

            // searchString
            if (!String.IsNullOrEmpty(searchString))
            {
                doctors = doctors.Where(s => s.DoctorName.Contains(searchString));
            }

            // order by name
            doctors = doctors.OrderBy(d => d.DoctorName);

            switch (sortOrder)
            {
                case "name_desc":
                    doctors = doctors.OrderByDescending(d => d.DoctorName);
                    break;
                default:
                    doctors = doctors.OrderBy(d => d.DoctorName);
                    break;
            }

            // setting pages
            int pageSize = 10;  // page size
            int pageNumber = (page ?? 1);
            return View(doctors.ToPagedList(pageNumber, pageSize));
        }



        // GET: DoctorDetails/Details/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorDetails doctorDetails = db.DoctorDetails.Find(id);
            if (doctorDetails == null)
            {
                return HttpNotFound();
            }
            return View(doctorDetails);
        }

        // GET: DoctorDetails/Create
        [Authorize(Roles = "Doctor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: DoctorDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Doctor")]
        public ActionResult Create([Bind(Include = "Id,DoctorName,PhoneNumber,Description,WorkingHours,Email")] DoctorDetails doctorDetails)
        {
            if (ModelState.IsValid)
            {
                db.DoctorDetails.Add(doctorDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(doctorDetails);
        }

        // GET: DoctorDetails/Edit/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorDetails doctorDetails = db.DoctorDetails.Find(id);
            if (doctorDetails == null)
            {
                return HttpNotFound();
            }
            return View(doctorDetails);
        }

        // POST: DoctorDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Edit([Bind(Include = "Id,DoctorName,PhoneNumber,Description,WorkingHours,Email")] DoctorDetails doctorDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctorDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctorDetails);
        }

        // GET: DoctorDetails/Delete/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DoctorDetails doctorDetails = db.DoctorDetails.Find(id);
            if (doctorDetails == null)
            {
                return HttpNotFound();
            }
            return View(doctorDetails);
        }

        // POST: DoctorDetails/Delete/5
        [Authorize(Roles = "Doctor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public ActionResult DeleteConfirmed(int id)
        {
            DoctorDetails doctorDetails = db.DoctorDetails.Find(id);
            db.DoctorDetails.Remove(doctorDetails);
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
