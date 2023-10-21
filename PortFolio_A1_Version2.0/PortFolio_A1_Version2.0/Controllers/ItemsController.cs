using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PortFolio_A1_Version2._0.Models;

namespace PortFolio_A1_Version2._0.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        // GET: Items
        [Authorize(Roles = "Patient,Doctor")]
        public ActionResult Index(string sortOrder, string searchString, int? page)
        {
            // Current sort order and search string for pagination
            ViewBag.CurrentSort = sortOrder;
            ViewBag.AgeSortParm = sortOrder == "age" ? "age_desc" : "age";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.CurrentFilter = searchString;

            var items = from s in db.Items
                        select s;

            // Search by name,Age,CreatedDate
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(s => s.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    items = items.OrderByDescending(s => s.Name);
                    break;

                case "age":
                    items = items.OrderBy(s => s.Age);
                    break;

                case "age_desc":
                    items = items.OrderByDescending(s => s.Age);
                    break;

                case "date":
                    items = items.OrderBy(s => s.CreatedDate);
                    break;

                case "date_desc":
                    items = items.OrderByDescending(s => s.CreatedDate);
                    break;

                case "price":  // Price Ascending
                    items = items.OrderBy(s => s.Price);
                    break;

                case "price_desc":  // Price Descending
                    items = items.OrderByDescending(s => s.Price);
                    break;

                default:
                    items = items.OrderBy(s => s.Name);
                    break;
            }


            // Pagination
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(items.ToPagedList(pageNumber, pageSize));
        }

        // GET: Items/Details/5
        [Authorize(Roles = "Patient,Doctor")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        [Authorize(Roles = "Patient,Doctor")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        [Authorize(Roles = "Patient,Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Price,Age,PhoneNumber,Address")] Item item)
        {
            if (ModelState.IsValid)
            {
                // Check Name is unique
                var existingItem = db.Items.FirstOrDefault(i => i.Name == item.Name);
                if (existingItem != null)
                {
                    ModelState.AddModelError("Name", "This item name already exists.");
                    return View(item);
                }

                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Edit/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        [Authorize(Roles = "Doctor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Price,Age,PhoneNumber,Address")] Item item)
        {
            if (ModelState.IsValid)
            {
                // Check if another item has the same name
                var anotherItemWithSameName = db.Items.FirstOrDefault(i => i.Name == item.Name && i.Id != item.Id);
                if (anotherItemWithSameName != null)
                {
                    ModelState.AddModelError("Name", "This item name already exists.");
                    return View(item);
                }

                var existingItem = db.Items.Find(item.Id); // Check Database

                if (existingItem != null)
                {
                    existingItem.Name = item.Name;          // 2. update attribute
                    existingItem.Description = item.Description;
                    existingItem.Price = item.Price;
                    
                    existingItem.Age = item.Age;
                    existingItem.PhoneNumber = item.PhoneNumber;
                    existingItem.Address = item.Address;

                    existingItem.LastModifiedDate = DateTime.Now;  // 3. Set up LastModifiedDate as current Date and time

                    db.Entry(existingItem).State = EntityState.Modified;
                    db.SaveChanges();                           // 4. Save changes

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Item not found.");
                }
            }
            return View(item);
        }


        // GET: Items/Delete/5
        [Authorize(Roles = "Doctor")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [Authorize(Roles = "Doctor")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
