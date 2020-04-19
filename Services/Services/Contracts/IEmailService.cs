using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services.Contracts
{
    public interface IEmailService
    {
        void SendEmail(string email, string subject, string message);
    }
}
