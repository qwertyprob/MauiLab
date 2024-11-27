using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telemedicine.Domain.Entities.Responses;

namespace Telemedicine.Domain.Entities.UserData
{
    public class UserMinimalData
    {
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Base64Photo { get; set; }

        public ResponseMessage Status { get; set; }
    }
}
