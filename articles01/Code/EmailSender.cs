using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace articles01.Code
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("menna.ahmed1000525@gmail.com", "sjgthraopmhxyeug"),
                EnableSsl = true,
            };
            return smtpClient.SendMailAsync("menna.ahmed1000525@gmail.com", email, subject, htmlMessage);
        }
    }
}
