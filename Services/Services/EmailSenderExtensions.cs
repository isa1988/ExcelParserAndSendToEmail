using Services.Dtos;
using Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public static class EmailSenderExtensions
    {
        public static void SendEmailMesage(this IEmailService emailSender, UserInfoDto userInfo, string topic, string message)
        {
            string sendMessage = GetMesageOnEmail(userInfo, message);
            if (sendMessage.Contains("\r\n"))
            {
                sendMessage = sendMessage.Replace("\r\n", "<br/>");
            }

            emailSender.SendEmail(userInfo.EMail, topic, sendMessage);
        }

        private static string GetMesageOnEmail(UserInfoDto userInfo, string message)
        {
            if (string.IsNullOrWhiteSpace(message)) return message;

            if (message.Contains("[Name]"))
                message = message.Replace("[Name]", GetValueAfterCheck(userInfo.Name));
            if (message.Contains("[SurName]"))
                message = message.Replace("[SurName]", GetValueAfterCheck(userInfo.SurName));
            if (message.Contains("[Email]"))
                message = message.Replace("[Email]", GetValueAfterCheck(userInfo.EMail));
            if (message.Contains("[Age]"))
                message = message.Replace("[Age]", GetValueAfterCheck(userInfo.Age.ToString()));
            
            return message;
        }

        private static string GetValueAfterCheck(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        }
    }
}
