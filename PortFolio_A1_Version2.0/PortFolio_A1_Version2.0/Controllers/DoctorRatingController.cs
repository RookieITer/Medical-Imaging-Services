using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PortFolio_A1_Version2._0.Models;
using PortFolio_A1_Version2._0.Models.ViewModel;

namespace PortFolio_A1_Version2._0.Controllers
{
    public class DoctorRatingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorRatingController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: DoctorRating
        public ActionResult Index()
        {
            var viewModel = new DoctorRatingViewModel
            {
                Doctors = _context.Doctors.Select(d => new SelectListItem
                {
                    Value = d.DoctorId.ToString(),
                    Text = d.Name
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult SubmitRating(DoctorRating rating)
        {
            // Get user id
            var userId = User.Identity.GetUserId();
            rating.UserId = userId; // UserId as log in user

            var viewModel = new DoctorRatingViewModel
            {
                Doctors = _context.Doctors.Select(d => new SelectListItem
                {
                    Value = d.DoctorId.ToString(),
                    Text = d.Name
                }).ToList()
            };

            if (!ModelState.IsValid)
            {
                viewModel.ErrorMessage = "Invalid data provided.";
                return View("Index", viewModel); 
            }

            var existingRating = _context.DoctorRatings
                .FirstOrDefault(r => r.UserId == rating.UserId && r.DoctorId == rating.DoctorId);

            if (existingRating != null)
            {
                viewModel.ErrorMessage = "You've already rated this doctor.";
                return View("Index", viewModel); 
            }

            _context.DoctorRatings.Add(rating);
            _context.SaveChanges();

            return RedirectToAction("Result");
        }


        [HttpGet]
        public ActionResult GetDoctorRating(int doctorId)
        {
            var ratings = _context.DoctorRatings.Where(r => r.DoctorId == doctorId).ToList();

            if (!ratings.Any())
            {
                return Json(new { success = false, message = "No ratings available for this doctor." }, JsonRequestBehavior.AllowGet);
            }

            var averageRating = ratings.Average(r => r.Score);
            return Json(new { success = true, averageRating }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Result()
        {
            var doctors = _context.Doctors.ToList();
            var ratings = _context.DoctorRatings.ToList();

            var doctorRatings = _context.Doctors.Select(d => new DoctorRatingViewModel
            {
                DoctorId = d.DoctorId,  
                DoctorName = d.Name,
                Score = _context.DoctorRatings.Where(r => r.DoctorId == d.DoctorId).DefaultIfEmpty().Average(r => r == null ? 0 : r.Score)
            }).ToList();

            return View(doctorRatings);
        }

    }
}
