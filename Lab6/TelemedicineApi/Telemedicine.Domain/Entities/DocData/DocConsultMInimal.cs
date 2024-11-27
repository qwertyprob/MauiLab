using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Domain.Entities.DocData
{
    public class DocConsultMinimal
    {
        public int ConsId { get; set; }
        public string Name { get; set; }
        public string Disease { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int DocId { get; set; }
        public bool IsConfirmed { get; set; }
    }
}
