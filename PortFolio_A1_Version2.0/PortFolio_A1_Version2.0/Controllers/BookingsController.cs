using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using PortFolio_A1_Version2._0.Models;
using Microsoft.AspNet.Identity;

namespace PortFolio_A1_Version2._0.Controllers
{
    [Authorize(Roles = "Patient")]
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Bookings
        public async Task<ActionResult> Index()
        {
            return View(await _context.Bookings.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookingId, ...")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                // Verify the time before submit
                System.Diagnostics.Debug.WriteLine("Submitted StartTime: " + booking.StartTime);
                System.Diagnostics.Debug.WriteLine("Submitted EndTime: " + booking.EndTime);

                if (!IsBookingConflict(booking.StartTime, booking.EndTime))
                {
                    // Connect Id
                    var currentUserId = User.Identity.GetUserId();
                    booking.UserId = currentUserId;

                    _context.Bookings.Add(booking);
                    await _context.SaveChangesAsync();

                    // verify the time after submit
                    var savedBooking = _context.Bookings.Find(booking.BookingId);
                    System.Diagnostics.Debug.WriteLine("Saved StartTime: " + savedBooking.StartTime);
                    System.Diagnostics.Debug.WriteLine("Saved EndTime: " + savedBooking.EndTime);

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "The chosen date and time conflicts with an existing booking.");
                }
            }
            return View(booking);
        }*/

        // GET: Bookings/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind(Include = "BookingId, ...")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                if (!IsBookingConflict(booking.StartTime, booking.EndTime, booking.BookingId))
                {
                    _context.Entry(booking).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "The chosen date and time conflicts with an existing booking.");
                }
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            var booking = await _context.Bookings.FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return HttpNotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IsBookingConflict(DateTime startDate, DateTime endDate, int? excludeBookingId = null)
        {
            var conflictingBookings = _context.Bookings.Where(b =>
                (startDate < b.EndTime && endDate > b.StartTime) &&
                (excludeBookingId == null || b.BookingId != excludeBookingId)).ToList();

            return conflictingBookings.Count > 0;
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }

        [HttpPost]
        public JsonResult GetBookings()
        {
            var bookings = _context.Bookings.Select(b => new
            {
                id = b.BookingId,
                start = b.StartTime,
                end = b.EndTime,
                title = "Booking #" + b.BookingId  
            }).ToList();

            return Json(bookings, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateBooking(DateTime startTime, DateTime endTime)
        {
            // Input verification
            if (startTime >= endTime)
            {
                return Json(new { success = false, message = "Start time must be earlier than end time." });
            }

            if (startTime < DateTime.Now)
            {
                return Json(new { success = false, message = "You can only make future booking from now on." });
            }

            // checking conflict
            var overlappingBooking = _context.Bookings
                .FirstOrDefault(b =>
                    (startTime < b.EndTime && endTime > b.StartTime) || // overlap
                    (startTime == b.StartTime && endTime == b.EndTime) // exactly matched
                );

            // overlappingbooking
            if (overlappingBooking != null)
            {
                return Json(new
                {
                    success = false,
                    message = "This booking time was already exist, please try another time",
                    conflictingBookingId = overlappingBooking.BookingId,
                    conflictingBookingStartTime = overlappingBooking.StartTime,
                    conflictingBookingEndTime = overlappingBooking.EndTime
                });
            }

            Booking newBooking = new Booking
            {
                StartTime = startTime,
                EndTime = endTime
            };

            try
            {
                _context.Bookings.Add(newBooking);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // mark this error
                return Json(new { success = false, message = "Failed to create booking, try again" });
            }

            return Json(new { success = true });
        }


    }
}
