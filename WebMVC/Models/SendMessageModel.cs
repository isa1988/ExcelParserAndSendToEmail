using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class SendMessageModel : PageInfoModel
    {
        public List<UserInfoModel> UserList { get; set; }

        public string Message { get; set; }

        public bool IsSend { get; set; }

        public SendMessageModel()
        {
            UserList = new List<UserInfoModel>();
        }
    }
}
