using Services.Dtos;
using Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class SendMessageService : ISendMessageService
    {
        public EntityOperationResult<SendMessageDto> Send(IEmailService emailSender, SendMessageDto sendMessage)
        {
            if (!sendMessage.IsSend)
                return EntityOperationResult<SendMessageDto>.Failure().AddError("Не была отмечана галочка отправить письма");

            try
            {
                var userInfoDtos = sendMessage.UserList.Where(x => x.IsSend).ToList();
                foreach (var user in userInfoDtos)
                {
                    emailSender.SendEmailMesage(user, sendMessage.Topic, sendMessage.Message);
                }
                return EntityOperationResult<SendMessageDto>.Success(new SendMessageDto());
            }
            catch(Exception ex)
            {
                return EntityOperationResult<SendMessageDto>.Failure().AddError(ex.Message);
            }
        }
    }
}
