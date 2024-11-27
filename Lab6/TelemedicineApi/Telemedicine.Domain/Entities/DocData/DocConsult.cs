using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Domain.Entities.DocData
{
   public class DocConsult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConsId { get; set; }

        [Required]
        [Display(Name = "Name")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Disease")]
        [StringLength(30)]
        public string Disease { get; set; }

        [Required]
        [Display(Name = "Address")]
        [StringLength(30)]
        public string Address { get; set; }

        [Display(Name = "Short Description")]
        [StringLength(260)]
        public string Description { get; set; }

        public int DocId { get; set; }

        public bool IsConfirmed { get; set; }
    }
}
