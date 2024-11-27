using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Domain.Entities.DocData
{
    public class DocProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocId { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(30)]
        public string FullName { get; set; }

        [Display(Name = "Activity Domain")]
        [StringLength(30)]
        public string Specs { get; set; }

        [Display(Name = "Address")]
        [StringLength(30)]
        public string Address { get; set; }

        [Display(Name = "Short Description")]
        [StringLength(260)]
        public string About { get; set; }

        [Display(Name = "Rate")]
        public double Stars { get; set; }
        public string Photo { get; set; }
        public bool IsActive { get; set; }
    }
}
