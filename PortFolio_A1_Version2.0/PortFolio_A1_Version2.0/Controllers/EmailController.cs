using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PortFolio_A1_Version2._0.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace PortFolio_A1_Version2._0.Controllers
{
    [RequireHttps]
    [Authorize]
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult SendEmailForm()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SendEmailForm(SendEmail model, HttpPostedFileBase attachmentFile)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return with validation errors
            }

            // Check attachment type
            var allowedExtensions = new List<string> { ".doc", ".docx", ".pdf", ".jpg", ".png", ".csv"}; // add more types if needed
            if (attachmentFile != null && !allowedExtensions.Contains(System.IO.Path.GetExtension(attachmentFile.FileName).ToLower()))
            {
                ModelState.AddModelError("", "Invalid file type.");
                return View(model);
            }

            await SendEmail(model.ToEmail, model.Subject, model.Content, attachmentFile);

            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            return View();
        }

        private async Task SendEmail(String toEmail, String subject, String contents, HttpPostedFileBase attachmentFile)
        {
            var apiKey = System.Configuration.ConfigurationManager.AppSettings["SendGridAPIKey"]; // retrieve from config or env variable
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("jiahuiduno1@outlook.com", "XMediHub");
            var to = new EmailAddress(toEmail, "");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            if (attachmentFile != null && attachmentFile.ContentLength > 0)
            {
                byte[] fileBytes = new byte[attachmentFile.InputStream.Length];
                await attachmentFile.InputStream.ReadAsync(fileBytes, 0, (int)attachmentFile.InputStream.Length);
                msg.AddAttachment(attachmentFile.FileName, Convert.ToBase64String(fileBytes));
            }

            await client.SendEmailAsync(msg);
        }

        // Send Bulk Email part
        private async Task SendBulkEmails(List<string> toEmails, string subject, string contents)
        {
            var apiKey = System.Configuration.ConfigurationManager.AppSettings["SendGridAPIKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("jiahuiduno1@outlook.com", "XMediHub");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";

            foreach (var email in toEmails)
            {
                var to = new EmailAddress(email, "");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                await client.SendEmailAsync(msg);
            }
        }

        public ActionResult SendBulkEmailForm()
        {
            using (var db = new ApplicationDbContext()) 
            {
                var doctors = db.DoctorDetails.ToList(); // Get doctor list
                return View(doctors);
            }
        }

        [HttpPost]
        public async Task<ActionResult> SendBulkEmailForm(List<string> selectedDoctors, string subject, string contents)
        {
            if (selectedDoctors != null && selectedDoctors.Any())
            {
                await SendBulkEmails(selectedDoctors, subject, contents);
            }

            return RedirectToAction("Success");
        }

    }
}
