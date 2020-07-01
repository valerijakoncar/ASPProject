using System;
using System.Collections.Generic;
using System.Text;

namespace ASPProjekat.Application.Email
{
    public interface IEmailSender
    {
        void Send(SendMailDto dto); 
    }

    public class SendMailDto
    {     
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
