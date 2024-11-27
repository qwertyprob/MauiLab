using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Domain.Entities.DocData
{
    public class DocMinimalProfile
    {
        public int DocId { get; set; }
        public string FullName { get; set; }
        public string Specs { get; set; }
        public string Address { get; set; }
        public string About { get; set; }
        public double Stars { get; set; }
        public string Photo { get; set; }
    }
}
