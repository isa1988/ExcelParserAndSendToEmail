using MailKit.Net.Smtp;
using MimeKit;
using Services.Dtos;
using Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class EmailServicecs : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailServicecs(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        public void SendEmail(string email, string subject, string message)
        {
            try
            {
                MimeMessage messageEmail = new MimeMessage();
                messageEmail.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender)); //отправитель сообщения
                messageEmail.To.Add(new MailboxAddress(email)); //адресат сообщения
                messageEmail.Subject = subject; //тема сообщения
                messageEmail.Body = new BodyBuilder() { HtmlBody = message, }.ToMessageBody(); //тело сообщения (так же в формате HTML)

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect(_emailSettings.MailServer, _emailSettings.MailPort, true); //либо использум порт 465
                    client.Authenticate(_emailSettings.Sender, _emailSettings.Password); //логин-пароль от аккаунта
                    client.Send(messageEmail);

                    client.Disconnect(true);
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}
