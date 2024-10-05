using EShop.Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Services.Implementations
{
    public class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;

        public EmailSender(ILogger<EmailSender> logger)
        {
            _logger = logger;
        }
        public bool SendEmail(string to, string subject, string body)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("MahdiMazhou74@gmail.com", "کد تایید فراموشی کلمه عبور");
                mail.To.Add(to);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpServer.Port = 587;
                SmtpServer.EnableSsl = true;

                SmtpServer.Credentials = new System.Net.NetworkCredential("MahdiMazhou74@gmail.com", "rgtg ksjh aikf usuw");
                SmtpServer.Send(mail);

                return  true;
            }
            catch (Exception exception)
            {
                _logger.LogError($"Email Error\n\tErrorMessage:: {exception.Message}");
                return  false;
            }
        }
    }
}
