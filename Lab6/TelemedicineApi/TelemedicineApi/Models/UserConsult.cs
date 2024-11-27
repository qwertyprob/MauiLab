using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TelemedicineApi.Models
{
    public class UserConsult
    {
        public string Name { get; set; }
        public string Disease { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
    }
}