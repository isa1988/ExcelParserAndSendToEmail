using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dtos
{
    public class UserInfoDto
    {
        public bool IsSend { get; set; }

        public string Name  { get; set; }

        public string SurName { get; set; }

        public int Age { get; set; }

        public string EMail { get; set; }
    }
}
