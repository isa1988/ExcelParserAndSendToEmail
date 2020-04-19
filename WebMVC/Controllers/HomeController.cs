using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Dtos;
using Services.Services.Contracts;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        IMapper _mapper;
        IExcelService _service;
        IEmailService _emailService;
        ISendMessageService _sendMessage;
        public HomeController(IExcelService service, ISendMessageService sendMessage, IEmailService emailService, IMapper mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            if (service == null)
                throw new ArgumentNullException(nameof(service));
            if (sendMessage == null)
                throw new ArgumentNullException(nameof(sendMessage));
            if (emailService == null)
                throw new ArgumentNullException(nameof(emailService));

            _service = service;
            _mapper = mapper;
            _sendMessage = sendMessage;
            _emailService = emailService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFile()
        {
            var model = new InportExcelModel();
            model.Message = "Dear [SurName] [Name]" + Environment.NewLine;
            model.Message += "your age [Age]";
            model.Topic = "Тема письма";
            model.IsSend = true;
            return View(model);
        }

        [HttpPost]
        public IActionResult AddFile(InportExcelModel request)
        {
            if (request.InportFile != null)
            {
                var ms = new MemoryStream();
                request.InportFile.OpenReadStream().CopyTo(ms);
                request.File = ms.ToArray();
            }
            var inportExcelDto = _mapper.Map<InportExcelDto>(request);
            
            var result = _service.Analysis(inportExcelDto);

            if (result.IsSuccess)
            {
                var sendMessageInfoModel = _mapper.Map<SendMessageModel>(request);
                sendMessageInfoModel.UserList = _mapper.Map<List<UserInfoModel>>(_service.MessageToUserList);

                return View("MessageToUsers", sendMessageInfoModel);
            }
            else
            {
                request.Error = GetError(result.Errors);
                return View(request);
            }
        }


        [HttpPost]
        public IActionResult MessageToUsers(SendMessageModel request)
        {
            var sendMessageInfoDto = _mapper.Map<SendMessageDto>(request);

            var result = _sendMessage.Send(_emailService, sendMessageInfoDto);

            if (result.IsSuccess)
            {
                return View("MessageSuccess");
            }
            else
            {
                request.Error = GetError(result.Errors);
                return View(request);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string GetError(string[] errors)
        {
            if (errors == null) return string.Empty;
            string error = string.Empty;
            for (int i = 0; i < errors.Length; i++)
            {
                error += errors[i] + Environment.NewLine;
            }
            return error;
        }
    }
}
