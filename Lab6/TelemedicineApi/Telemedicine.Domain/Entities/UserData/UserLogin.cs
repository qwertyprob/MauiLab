using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Domain.Entities.UserData
{
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string LoginIp { get; set; }
        public DateTime LoginDateTime { get; set; }
    }
}
