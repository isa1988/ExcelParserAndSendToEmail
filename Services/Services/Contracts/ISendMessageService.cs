using Services.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services.Contracts
{
    public interface ISendMessageService
    {
        EntityOperationResult<SendMessageDto> Send(IEmailService emailSender, SendMessageDto sendMessage);
    }
}
