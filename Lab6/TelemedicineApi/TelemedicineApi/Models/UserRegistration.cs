using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TelemedicineApi.Models
{
    public class UserRegistration
    {
        [Required]
        [Display(Name = "Full Name")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Full Name cannot be longer than 30 characters.")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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

        [Compare("Password", ErrorMessage = "Confirm password does not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat Password")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        public string Base64Photo { get; set; }
    }
}