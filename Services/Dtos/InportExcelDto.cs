using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class InportExcelDto
    {
        public string Message { get; set; }

        public string Topic { get; set; }

        public bool IsSend { get; set; }

        public byte[] File { get; set; }
    }
}
