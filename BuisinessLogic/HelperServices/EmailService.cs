using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using DomainLayer.User;
using System.Text.Encodings.Web;

namespace BuisinessLogic.HelperServices
{

    public interface IEmailService
    {
        void SendConfirmationEmail(User user, string callBackUrl);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public void SendConfirmationEmail(User user, string callBackUrl)
        {
            string message = File.ReadAllText("../BuisinessLogic/Resources/ConfirmationEmail.html");
            message = message.Replace("X1num1X", HtmlEncoder.Default.Encode(callBackUrl));
            string subject = "Wallet API Email Confirmation";
            SendEmail(user.Email, subject, message);
        }

        private void SendEmail(string email, string subject, string message)
        {
            try
            {
                MailMessage msg = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                msg.From = new MailAddress(_configuration["Email:Email"]);
                msg.To.Add(new MailAddress(email));

                msg.Subject = subject;
                msg.IsBodyHtml = true;
                msg.Body = message;

                smtp.Port = int.Parse(_configuration["Email:Port"]);
                smtp.Host = _configuration["Email:Host"];
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_configuration["Email:Email"], _configuration["Email:Password"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace + "\n" + e.Message);
            }
        }

    }
}
