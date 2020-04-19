using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class InportExcelModel : PageInfoModel
    {
        [DisplayName("Сообщение")]
        public string Message { get; set; }

        [DisplayName("Отправить")]
        public bool IsSend { get; set; }

        [DisplayName("Тема")]
        public string Topic { get; set; }

        public byte[] File { get; set; }

        public IFormFile InportFile { get; set; }
    }
}
