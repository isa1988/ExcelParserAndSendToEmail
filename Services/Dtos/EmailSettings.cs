using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class EmailSettings
    {
        public EmailSettings(IConfiguration iConfiguration)
        {
            var smtpSection = iConfiguration.GetSection("EmailSettings");
            if (smtpSection != null)
            {
                int port = 25;
                MailServer = smtpSection.GetSection("MailServer").Value;
                int.TryParse(smtpSection.GetSection("MailPort").Value, out port);
                MailPort = port;
                SenderName = smtpSection.GetSection("SenderName").Value;
                Sender = smtpSection.GetSection("Sender").Value;
                Password = smtpSection.GetSection("Password").Value;
            }
        }

        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
    }
}
