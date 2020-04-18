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
        public HomeController(IExcelService service, IMapper mapper)
        {
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            if (service == null)
                throw new ArgumentNullException(nameof(service));

            _service = service;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddFile()
        {
            return View();
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
            var sendMessageInfoDto = _mapper.Map<InportExcelDto>(request);
            
            var result = _service.Analysis(sendMessageInfoDto);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index");
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
