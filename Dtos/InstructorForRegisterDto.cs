using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tennis_Mate.Dtos
{
    public class InstructorForRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Password must be more than four characters")]
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string CurrentEmployer { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

    }
}
