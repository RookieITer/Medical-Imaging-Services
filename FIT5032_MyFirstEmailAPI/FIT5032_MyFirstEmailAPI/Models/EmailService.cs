using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Web.Configuration;

namespace FIT5032_MyFirstEmailAPI.Models
{
    public class EmailService
    {
        public void SendEmail()
        {
            var client = new SendGridClient("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");
            var from = new EmailAddress("jiahuiduno1@outlook.com", "Test User");
            var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress("jduu0036@student.monash.edu", "Test User");
            var plainTextContent = "and easy to do anywhere, even with C#";
            var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            Attachment attachment = new Attachment();
            attachment.Filename = "Attached File";

            attachment.Type = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
            byte[] bytes = System.Text.Encoding.Default.GetBytes("This is a word attachment.");
            attachment.Content = Convert.ToBase64String(bytes);
            msg.AddAttachment(attachment);

            var response = client.SendEmailAsync(msg);
        }
    }
}