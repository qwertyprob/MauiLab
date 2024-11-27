using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telemedicine.Domain.Entities.UserData
{
   public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Full Name cannot be longer than 30 characters.")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [StringLength(30)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Location/Address")]
        [StringLength(30)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Username cannot be longer than 30 characters.")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Password")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password cannot be shorter than 8 characters.")]
        public string Password { get; set; }

        [Display(Name = "Photo")]
        public string Photo { get; set; }

        [NotMapped]
        public string Base64Photo { get; set; }

        [NotMapped]
        public string ServerPath { get; set; }


        [DataType(DataType.Date)]
        public DateTime RegDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime LastLogin { get; set; }

        [StringLength(30)]
        public string LasIp { get; set; }
    }
}
