using ASPProjekat.Application.Email;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ASPProjekat.Implementation.Email
{
    public class SmtpEmailSender : IEmailSender
    {
        public void Send(SendMailDto dto)
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("aspictrs@gmail.com", "Sifra123!")//sa koje adrese se salje mejl
            };

            var message = new MailMessage("aspictrs@gmail.com", dto.SendTo);// sa koje adrese se salje i kome
            message.Subject = dto.Subject;
            message.Body = dto.Content;
            message.IsBodyHtml = true;
            smtp.Send(message);
        }
    }
}
