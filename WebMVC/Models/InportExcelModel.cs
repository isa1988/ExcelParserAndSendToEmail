using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class InportExcelModel : PageInfoModel
    {
        public string Message { get; set; }

        public bool IsSend { get; set; }

        public byte[] File { get; set; }

        public IFormFile InportFile { get; set; }
    }
}
