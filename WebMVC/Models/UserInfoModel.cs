using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class UserInfoModel : PageInfoModel
    {
        [DisplayName("Отправить")]
        public bool IsSend { get; set; }

        [DisplayName("Имя")]
        public string Name { get; set; }

        [DisplayName("Фамилия")]
        public string SurName { get; set; }

        [DisplayName("Возраст")]
        public int Age { get; set; }

        [DisplayName("Почта")]
        public string EMail { get; set; }
    }
}
